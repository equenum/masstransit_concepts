using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Models;
using OrderService.Api.Models.Requests;

namespace OrderService.Api.Controllers;

[ApiController]
[Route("api/public/[controller]")]
public class OrdersController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(Database.Orders);
    }

    [HttpGet("{id:int}", Name = "GetById")]
    public IActionResult Get(int id)
    {
        var order = Database.Orders.FirstOrDefault(o => o.Id == id);
        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateOrderRequest request)
    {
        var latestId = Database.Orders.Max(order => order.Id);
        var order = new Order()
        {
            Id = latestId + 1,
            CustomerId = request.CustomerId,
            Type = request.Type,
            DeliveryDetails = request.DeliveryDetails,
            Products = request.Products
        };

        Database.Orders.Add(order);

        return CreatedAtRoute("GetById", new { id = order.Id }, order);
    }
}
