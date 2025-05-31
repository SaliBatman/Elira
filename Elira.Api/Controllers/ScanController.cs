using Elira.Api.Domain.Scan;
using Elira.Api.Dto;
using Elira.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ScanController(ScanService scanService) : ControllerBase
{
    private readonly ScanService _scanService = scanService;
    
    [HttpPost("newsubmit")]
    public async Task<Result<ScanRequests>> SubmitScanForm([FromBody] ScanFormDto request)
    {
        if (string.IsNullOrWhiteSpace(request.CustomerName) || string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            return Result<ScanRequests>.Failure("Please provide valid customer name, phone number or email address.");
        }

        var model = await _scanService.AddNewScan(request);
        return Result<ScanRequests>.Success(model);
    }
}