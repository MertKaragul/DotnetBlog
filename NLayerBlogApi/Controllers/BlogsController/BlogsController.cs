using AutoMapper;
using Core.Dtos.BlogDto;
using Core.Entities;
using Core.Enums;
using Core.Service;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayerBlogApi.Controllers.GenericController;
using System.Security.Claims;

namespace NLayerBlogApi.Controllers.BlogsController
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsController : CustomGenericController
    {
        private readonly IGenericService<Blog> _genericService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBlogDto> _createBlogValidator;
        private readonly IJwtService _jwtService;
        

        public BlogsController(
            IGenericService<Blog> genericService,
            IMapper mapper,
            IValidator<CreateBlogDto> createBlogValidator,
            IJwtService jwtService
            )
        {
            _genericService = genericService;
            _mapper = mapper;
            _createBlogValidator = createBlogValidator;
            _jwtService = jwtService;
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

        [Authorize("ADMIN")]
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
                return CustomResponse( new Core.Dtos.CustomResponseDto<IEnumerable<string>>
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
            await _genericService.AddAsync(convert);
            return CustomResponse( new Core.Dtos.CustomResponseDto<string>
            {
                Data = "Blog Created",
                StatusCode = 200
            } );
        }



    }
}
