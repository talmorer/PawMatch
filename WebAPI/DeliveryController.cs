using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        [HttpPost("CreateDelivery")]
        public IActionResult CreateDelivery([FromQuery] int petId, [FromQuery] int ownerId, [FromQuery] int adopterId)
        {
            try
            {
                string trackingNum = "DLY-" + petId + "-" + DateTime.Now.Millisecond;

                var result = new
                {
                    Success = true,
                    Message = "Delivery order received and confirmed successfully!",
                    TrackingNumber = trackingNum,
                    EstimatedCost = 45.00
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
    }
}