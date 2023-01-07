using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTierApp.Core.ResultPattern;

namespace NTierApp.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction] // swagger'ın endpoint olarak algılamaması için
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> responseDto) where T : class
        {
            // NO Data
            if(responseDto.StatusCode == 204)
            {
                return new ObjectResult(null)
                {
                    StatusCode = responseDto.StatusCode
                };
            
            }
            // Has Data
            return new ObjectResult(responseDto)
            {
                StatusCode = responseDto.StatusCode
            };


        }
    }
}
