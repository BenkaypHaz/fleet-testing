using Library.Infraestructure.Common.Filters.Authorization;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

[Route("api/[controller]")]
public class BaseControllerCustomers : ControllerBase
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly ILogger<BaseControllerCustomers> _logger;
    protected readonly IHttpContextAccessor _httpContextAccessor;

    public BaseControllerCustomers(ILogger<BaseControllerCustomers> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    protected long _customerId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Actor)?.Value;
            if (string.IsNullOrEmpty(value) || !long.TryParse(value, out var customerId))
                throw new UnauthorizedAccessException("Cliente no autenticado o claim inválido.");
            return customerId;
        }
    }
}
