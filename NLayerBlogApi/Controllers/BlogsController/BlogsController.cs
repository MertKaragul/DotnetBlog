using AutoMapper;
using Core.Dtos;
using Core.Dtos.BlogDto;
using Core.Entities;
using Core.Service;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLayerBlogApi.Controllers.GenericController;
using System.Runtime.InteropServices.ObjectiveC;
using System.Security.Claims;

namespace NLayerBlogApi.Controllers.BlogsController {
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsController : CustomGenericController
    {
        private readonly IGenericService<Blog> _genericService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBlogDto> _createBlogValidator;
        private readonly IJwtService _jwtService;
        private readonly IGenericService<User> _userService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public BlogsController(
            IGenericService<Blog> genericService,
            IMapper mapper,
            IValidator<CreateBlogDto> createBlogValidator,
            IJwtService jwtService,
            IGenericService<User> userService,
            IWebHostEnvironment hostingEnvironment
            )
        {
            _genericService = genericService;
            _mapper = mapper;
            _createBlogValidator = createBlogValidator;
            _jwtService = jwtService;
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult> GetBlogs()
        {

            var getBlogs = await _genericService.GetAll()
                .Include(x => x.User)
                .Include(x => x.Comments)
                .Include(x => x.Tags)
                .ToListAsync();
            var blogDto = _mapper.Map<IEnumerable<BlogDto>>(getBlogs);

            return CustomResponse(
                new Core.Dtos.CustomResponseDto<IEnumerable<BlogDto>>
                {
                    Data = blogDto,
                    StatusCode = 200
                }
            );
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult> CreateBlog(CreateBlogDto? createBlogDto)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (accessToken == null)
            {
                return CustomResponse(new Core.Dtos.CustomResponseDto<IEnumerable<string>>
                {
                    Errors = new List<String>
                    {
                        "Token required"
                    },
                    StatusCode = 400
                });
            }


            var validate = await _createBlogValidator.ValidateAsync(createBlogDto);
            if (!validate.IsValid)
            {
                return CustomResponse( new CustomResponseDto<IEnumerable<string>>
                {
                    Errors = validate.Errors.Select(x => x.ErrorMessage).ToList(),
                    StatusCode = 400
                });
            }


            var validateToken = await _jwtService.ValidateToken(accessToken);

            if (validateToken == null)
            {
                return CustomResponse(new Core.Dtos.CustomResponseDto<string>
                {
                    Errors = new List<String>
                    {
                        "Token required"
                    },
                    StatusCode = 401
                });
            }

            var userId = validateToken[ClaimTypes.NameIdentifier];

            if (userId == null)
            {
                return CustomResponse(new Core.Dtos.CustomResponseDto<string>
                {
                    Errors = new List<String>
                    {
                        "Unauthorize"
                    },
                    StatusCode = 401
                });
            }


            var convert = _mapper.Map<Blog>(createBlogDto);
            convert.UserId = int.Parse(userId.ToString() ?? "0");

            // Upload image
            byte[] image = Convert.FromBase64String(convert.Image);
            string fileName = Guid.NewGuid().ToString() + ".png";
            string filePath = Path.Combine( _hostingEnvironment.ContentRootPath + "/images" , fileName);
            System.IO.File.WriteAllBytes(filePath, image);



            await _genericService.AddAsync(convert);
            return CustomResponse( new CustomResponseDto<string>
            {
                Data = "Blog Created",
                StatusCode = 200
            } );
        }



        [HttpGet("{title}")]
        public async Task<ActionResult> GetBlogFromTitle(string? title)
        {
            if(title.IsNullOrEmpty())
            {
                return CustomResponse(new CustomResponseDto<string>
                {
                    Errors = new List<string>
                    {
                        "Title required"
                    },
                    StatusCode = 400
                });
            }

            var data = await _genericService
                .Where(x => x.BlogTitle.Contains(title ?? ""))
                .Include(x => x.User)
                .Include(x => x.Comments)
                .Include(x => x.Tags)
                .ToListAsync();
            
            return CustomResponse(new CustomResponseDto<List<BlogDto>>
            {
                Data = _mapper.Map<List<BlogDto>>(data),
                StatusCode = 200
            });
        }

        [HttpDelete("{blogId}")]
        [Authorize]
        public async Task<ActionResult> DeletePost(int? blogId)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if(accessToken == null)
            {
                return CustomResponse(new CustomResponseDto<IEnumerable<string>>
                {
                    Errors = new List<String>
                    {
                        "Token required"
                    },
                    StatusCode = 400
                });
            }


            var validateToken = await _jwtService.ValidateToken(accessToken);

            if(validateToken == null)
            {
                return CustomResponse(new Core.Dtos.CustomResponseDto<string>
                {
                    Errors = new List<String>
                    {
                        "Token required"
                    },
                    StatusCode = 401
                });
            }

            validateToken.TryGetValue(ClaimTypes.NameIdentifier, out object? value);
            if(value == null)
            {
                return CustomResponse(new Core.Dtos.CustomResponseDto<string>
                {
                    Errors = new List<String>
                    {
                        "Token required"
                    },
                    StatusCode = 401
                });
            }
            int.TryParse((string?)value, out int data);

            var findBlog = await _genericService.Where(x => x.BlogId == blogId && x.UserId == data).FirstOrDefaultAsync();

            if(findBlog == null)
            {
                return CustomResponse(new Core.Dtos.CustomResponseDto<string>
                {
                    Errors = new List<String>
                    {
                        "Blog not found"
                    },
                    StatusCode = 400
                });
            }

            _genericService.Delete(findBlog);

            return CustomResponse(new Core.Dtos.CustomResponseDto<string>
            {
                Errors = new List<String>
                    {
                        "Blog deleted"
                    },
                StatusCode = 200
            });
        }
    }
}
