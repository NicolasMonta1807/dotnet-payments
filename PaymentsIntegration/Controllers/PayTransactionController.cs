using Microsoft.AspNetCore.Mvc;
using PaymentsIntegration.Model.DTOs;
using PaymentsIntegration.Services;

namespace PaymentsIntegration.Controllers;

[ApiController]
[Route("[controller]")]
public class PayTransactionController : ControllerBase
{
    private readonly PayTransactionService _payTransactionService;

    public PayTransactionController(PayTransactionService payTransactionService)
    {
        _payTransactionService = payTransactionService;
    }

    [HttpPost]
    public ActionResult<PaymentResponse> PostPaymentRequest([FromBody] PaymentRequest request)
    {
        var response = _payTransactionService.ProcessPayment(request);

        if (!response.Success)
            return BadRequest(new
            {
                message = response.Message,
                transactionId = response.TransactionId
            });

        return Ok(response);
    }
}