using Microsoft.AspNetCore.Mvc;

namespace MonolithicPersistence.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonoController(IConfiguration configuration, IDataAccess dataAccess) : ControllerBase
    {
        [HttpPost()]
        public async Task<IActionResult> PostAsync()
        {
            var connectionStr = configuration["AdventureWorksProductDB"];
            await dataAccess.InsertPurchaseOrderHeaderAsync(connectionStr);

            await dataAccess.LogAsync(connectionStr, "ErrorLog");

            return Ok();
        }
    }
}
