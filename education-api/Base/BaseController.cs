using education_infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace education_api.Base
{
    public class BaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(GenericResponse<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = (int?)response.StatusCode
            };
        }
    }
}
