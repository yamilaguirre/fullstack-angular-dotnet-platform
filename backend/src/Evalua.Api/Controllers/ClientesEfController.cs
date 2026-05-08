using Evalua.Api.Contracts;
using Evalua.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evalua.Api.Controllers;

[ApiController]
[Route("api/clientes/ef")]
public class ClientesEfController : ControllerBase
{
    private readonly ClientesEntityFrameworkQuery _query;

    public ClientesEfController(ClientesEntityFrameworkQuery query)
    {
        _query = query;
    }

    /// <summary>
    /// Returns clients + country using EF Core query operators (Include + Skip/Take + projection).
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedResponse<ClienteDto>>> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _query.GetPaginatedAsync(page, pageSize, cancellationToken);
        return Ok(result);
    }
}
