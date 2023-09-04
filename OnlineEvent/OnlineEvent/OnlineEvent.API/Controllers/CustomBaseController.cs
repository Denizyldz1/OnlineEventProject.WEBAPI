using Microsoft.AspNetCore.Mvc;
using OnlineEvent.Model;

namespace OnlineEvent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction] // Bu Method bir endpoint değil swagger bunu algılamaması için NonAction attirbute kullanıyoruz.
        public IActionResult CreateActionResult<T>(CustomResponseModel<T> response)
        {
            if (response.StatusCode == 204)
            {
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode,
                };
            }
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };
        }

    }
}
