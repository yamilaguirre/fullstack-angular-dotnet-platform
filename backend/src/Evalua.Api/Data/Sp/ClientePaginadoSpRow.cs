namespace Evalua.Api.Data.Sp;

/// <summary>
/// Keyless projection mapped to the result set returned by <c>dbo.usp_ClientesPaginados</c>.
/// </summary>
public class ClientePaginadoSpRow
{
    public int IdCliente { get; set; }

    public string NombreCompleto { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public int IdPais { get; set; }

    public string NombrePais { get; set; } = string.Empty;

    public long TotalRegistros { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}
