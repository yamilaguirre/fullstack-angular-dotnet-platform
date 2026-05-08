namespace Evalua.Api.Entities;

public class Pais
{
    public int IdPais { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
