using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Models.Requests;
using Shared.Common.DataAccess;
using Shared.Common.Messages;

namespace OrderService.Api.Controllers;

[ApiController]
[Route("api/public/[controller]")]
public class OrdersController : ControllerBase
{
    // private readonly IBus _bus; // standalone
    // private readonly IPublishEndpoint _endpoint; // within a container scope
    private readonly IRequestClient<RegisterOrder> _requestClient;

    public OrdersController(
        // IBus bus, 
        //IPublishEndpoint endpoint,
        IRequestClient<RegisterOrder> requestClient)
    {
        // _bus = bus;
        //_endpoint = endpoint;
        _requestClient = requestClient;
    }

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
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        int orderId;

        try
        {
            // await _endpoint.Publish(new RegisterOrder()
            // {
            //     CustomerId = request.CustomerId,
            //     Type = request.Type,
            //     DeliveryDetails = request.DeliveryDetails,
            //     Products = request.Products
            // }, 
            // sendContext => 
            // {
            //     sendContext.CorrelationId = Guid.NewGuid();
            // });

            var response = await _requestClient.GetResponse<OrderRegistrationResult>(new RegisterOrder()
            {
                CustomerId = request.CustomerId,
                DeliveryDetails = request.DeliveryDetails,
                Products = request.Products
            });

            orderId = response.Message.OrderId;
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

        return CreatedAtRoute("GetById", new { id = orderId }, orderId);
    }
}
