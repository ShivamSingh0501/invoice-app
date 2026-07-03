using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        /// <summary>
        /// Simple health/data check endpoint.
        /// </summary>
        [HttpGet]
        public IActionResult GetData()
        {
            // Previously: "string result = null;" then checked result.Length > 0
            // which threw a NullReferenceException.
            // Fixed: use a real, non-null string.
            string result = "Data fetched";

            if (result.Length > 0)
            {
                return Ok(new { message = result });
            }
            return BadRequest("No data");
        }
    }
}
