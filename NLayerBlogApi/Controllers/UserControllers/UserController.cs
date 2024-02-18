using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Service;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLayerBlogApi.Controllers.GenericController;

namespace NLayerBlogApi.Controllers.UserControllers {

    [Route("api/[controller]")]
    [ApiController]
    public class UserController: CustomGenericController {


        private readonly IMapper _mapper;
        private readonly IValidator<User> _validator;
        private readonly IGenericService<User> _genericService;

        public UserController(IGenericService<User> genericService, IMapper mapper,IValidator<User> validator)
        {
            _genericService = genericService;
            _mapper = mapper;
            _validator = validator;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult Login(LoginDto loginDto)
        {
            var convertDto = _mapper.Map<User>(loginDto);
            var validate = _validator.Validate(convertDto);

            var response = CustomResponseDto<List<string>>.Failure(errors: validate.Errors.Select(x => x.ErrorMessage).ToList(), statusCode: 400 );

            if(validate.IsValid){
                var success = CustomResponseDto<User>.Success(data: convertDto , statusCode: 200);

                return CustomResponse(success);
            }

            return CustomResponse(response);
        }




    }
}
