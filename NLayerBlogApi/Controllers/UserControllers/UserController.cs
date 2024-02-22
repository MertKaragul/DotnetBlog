using AutoMapper;
using Core.Dtos;
using Core.Dtos.JwtResponse;
using Core.Entities;
using Core.Service;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLayerBlogApi.Controllers.GenericController;
using System.Security.Claims;

namespace NLayerBlogApi.Controllers.UserControllers {

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController: CustomGenericController {


        private readonly IMapper _mapper;
        private readonly IValidator<User> _validator;
        private readonly IGenericService<User> _userService;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;

        public UserController(IGenericService<User> userService, 
            IMapper mapper,
            IValidator<User> validator,
            IJwtService jwtService,
            IConfiguration configuration)
        {
            _userService = userService;
            _mapper = mapper;
            _validator = validator;
            _jwtService = jwtService;
            _configuration = configuration;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginDto? loginDto)
        {
            var convertDto = _mapper.Map<User>(loginDto);
            var validate = _validator.Validate(convertDto);

            if(!validate.IsValid){
                return CustomResponse(CustomResponseDto<List<string>>.Failure(errors: validate.Errors.Select(x => x.ErrorMessage).ToList(), statusCode: 401));
            }

            var findUser = await _userService
                .Where(x => x.Email.ToLower().Trim().Contains(loginDto.Email.Trim().ToLower()) && x.Password.Trim().Contains(loginDto.Password.Trim()))
                .FirstOrDefaultAsync();

            if(findUser == null)
            {
                return CustomResponse(CustomResponseDto<List<string>>.Failure(errors: new List<string> { "User not found" } , statusCode: 401));
            }

            // Claims
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, findUser.UserId.ToString()),
                new Claim(ClaimTypes.Name, findUser.UserName.ToString()),
                new Claim(ClaimTypes.Email, findUser.Email.ToString()),
                new Claim(ClaimTypes.Role, findUser.Roles.ToString()),

            };

            // Access token
            var accessToken = _jwtService.CreateToken(userClaims, DateTime.Now.AddDays(int.Parse(_configuration["JWT:Expire"])));
            // RefreshToken
            userClaims.Add(new Claim("IsRefreshToken", "true"));
            var refreshToken = _jwtService.CreateToken(userClaims, DateTime.Now.AddDays(int.Parse(_configuration["JWT:RefExpire"])));



            return CustomResponse(
                CustomResponseDto<TokenResponseDto>.Success(
                    data: new TokenResponseDto { 
                        AccessToken = accessToken,
                        RefreshToken = refreshToken},
                    statusCode: 200));
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult ValidateUser(string? token)
        {
            if(token == null)
            {
                return BadRequest();
            }
            var validate = _jwtService.ValidateToken(token);
            if(!validate.IsNullOrEmpty())
            {
                return Unauthorized();
            }
            return Ok();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult CreateAccessToken(string? refreshToken)
        {
            if(refreshToken == null) return BadRequest();
            
            var validateToken = _jwtService.ValidateToken(refreshToken);

            if(!validateToken.IsNullOrEmpty() || validateToken == null)
            {
                return Unauthorized();
            }

            if(validateToken.FirstOrDefault(x => x.ValueType.Contains("IsRefreshToken")) == null)
            {
                return BadRequest("Just only refresh-token sendable");
            }

            var generateToken = _jwtService.CreateToken(validateToken, DateTime.Now.AddDays(int.Parse(_configuration["JWT:Expire"])));
            return Ok(generateToken);
        }




    }
}
