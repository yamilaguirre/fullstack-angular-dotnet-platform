namespace Evalua.Api.Entities;

public class Cliente
{
    public int IdCliente { get; set; }

    public string NombreCompleto { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public int IdPais { get; set; }

    public Pais Pais { get; set; } = null!;
}
