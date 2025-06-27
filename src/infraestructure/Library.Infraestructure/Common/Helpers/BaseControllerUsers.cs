using Library.Infraestructure.Common.Filters.Authorization;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

[Route("api/[controller]")]
public class BaseControllerUsers : ControllerBase
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly ILogger<BaseControllerUsers> _logger;
    protected readonly IHttpContextAccessor _httpContextAccessor;

    public BaseControllerUsers(ILogger<BaseControllerUsers> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    protected long _userId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(value) || !long.TryParse(value, out var userId))
                throw new UnauthorizedAccessException("Usuario no autenticado o claim inválido.");
            return userId;
        }
    }
}
