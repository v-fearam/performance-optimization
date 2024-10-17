using Microsoft.AspNetCore.Mvc;

namespace MonolithicPersistence.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolyController(IConfiguration configuration, IDataAccess dataAccess) : ControllerBase
    {
        [HttpPost()]
        public async Task<IActionResult> PostAsync()
        {
            var connectionStr = configuration["AdventureWorksProductDB"];
            var logConnectionStr = configuration["AdventureWorksLogProductDB"];
            await dataAccess.InsertPurchaseOrderHeaderAsync(connectionStr);

            await dataAccess.LogAsync(logConnectionStr, "ErrorLog");

            return Ok();
        }
    }
}
