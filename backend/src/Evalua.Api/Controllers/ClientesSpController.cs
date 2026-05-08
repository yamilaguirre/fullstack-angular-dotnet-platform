using Evalua.Api.Contracts;
using Evalua.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evalua.Api.Controllers;

[ApiController]
[Route("api/clientes/sp")]
public class ClientesSpController : ControllerBase
{
    private readonly ClientesStoredProcedureQuery _query;

    public ClientesSpController(ClientesStoredProcedureQuery query)
    {
        _query = query;
    }

    /// <summary>
    /// Returns clients joined with country via <c>dbo.usp_ClientesPaginados</c>.
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
