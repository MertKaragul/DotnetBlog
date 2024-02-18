using Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace NLayerBlogApi.Controllers.GenericController {
    public class CustomGenericController : ControllerBase {

        [NonAction]
        public JsonResult CustomResponse<T>(CustomResponseDto<T> customResponse)
        {
            return new JsonResult(customResponse)
            {
                StatusCode = customResponse.StatusCode
            };

        }

    }
}
