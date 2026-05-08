using Evalua.Api.Contracts;
using Evalua.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Evalua.Api.Services;

public class ClientesEntityFrameworkQuery
{
    private readonly AppDbContext _dbContext;

    public ClientesEntityFrameworkQuery(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<ClienteDto>> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var (safePage, safeSize) = NormalizePagination(page, pageSize);
        var skip = (safePage - 1) * safeSize;

        var baseQuery = _dbContext.Clientes
            .AsNoTracking()
            .Include(c => c.Pais)
            .OrderBy(c => c.IdCliente);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var items = await baseQuery
            .Skip(skip)
            .Take(safeSize)
            .Select(c => new ClienteDto(
                c.IdCliente,
                c.NombreCompleto,
                c.Telefono,
                c.IdPais,
                c.Pais.Nombre))
            .ToListAsync(cancellationToken);

        return new PagedResponse<ClienteDto>(items, safePage, safeSize, totalCount);
    }

    private static (int Page, int PageSize) NormalizePagination(int page, int pageSize)
    {
        var safePage = page < 1 ? 1 : page;
        var safeSize = pageSize < 1 ? 10 : Math.Min(pageSize, 500);
        return (safePage, safeSize);
    }
}
