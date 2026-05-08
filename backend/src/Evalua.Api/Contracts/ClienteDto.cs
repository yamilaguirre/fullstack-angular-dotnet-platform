namespace Evalua.Api.Contracts;

public record ClienteDto(
    int IdCliente,
    string NombreCompleto,
    string Telefono,
    int IdPais,
    string NombrePais);
