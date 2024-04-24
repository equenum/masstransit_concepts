using Microsoft.AspNetCore.Mvc;

namespace OrderService.Api;

[ApiController]
[Route("api/public/[controller]")]
public class OrdersController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(Database.Orders);
    }
}
