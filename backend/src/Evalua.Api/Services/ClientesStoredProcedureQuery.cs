using Evalua.Api.Contracts;
using Evalua.Api.Data;
using Evalua.Api.Data.Sp;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Evalua.Api.Services;

public class ClientesStoredProcedureQuery
{
    private readonly AppDbContext _dbContext;

    public ClientesStoredProcedureQuery(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<ClienteDto>> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var (safePage, safeSize) = NormalizePagination(page, pageSize);

        var pPage = new SqlParameter("PageNumber", safePage);
        var pSize = new SqlParameter("PageSize", safeSize);

        var rows = await _dbContext
            .Set<ClientePaginadoSpRow>()
            .FromSqlRaw("EXEC dbo.usp_ClientesPaginados @PageNumber, @PageSize", pPage, pSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var totalCount = rows.Count == 0 ? 0 : Convert.ToInt32(rows[0].TotalRegistros);

        var items = rows
            .Select(r => new ClienteDto(
                r.IdCliente,
                r.NombreCompleto,
                r.Telefono,
                r.IdPais,
                r.NombrePais))
            .ToList();

        return new PagedResponse<ClienteDto>(items, safePage, safeSize, totalCount);
    }

    private static (int Page, int PageSize) NormalizePagination(int page, int pageSize)
    {
        var safePage = page < 1 ? 1 : page;
        var safeSize = pageSize < 1 ? 10 : Math.Min(pageSize, 500);
        return (safePage, safeSize);
    }
}
