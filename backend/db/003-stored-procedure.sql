-- Paginated list: clients + country name.
-- Returns one rowset; TotalRegistros is repeated on each row (window aggregate).

IF OBJECT_ID(N'dbo.usp_ClientesPaginados', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_ClientesPaginados;
GO

CREATE PROCEDURE dbo.usp_ClientesPaginados @PageNumber INT,
                                           @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SafePageNumber INT =
                CASE
                    WHEN @PageNumber IS NULL OR @PageNumber < 1 THEN 1
                    ELSE @PageNumber
                END;
    DECLARE @SafePageSize INT =
                CASE
                    WHEN @PageSize IS NULL OR @PageSize < 1 THEN 10
                    WHEN @PageSize > 500 THEN 500
                    ELSE @PageSize
                END;
    DECLARE @Offset INT = (@SafePageNumber - 1) * @SafePageSize;

    SELECT c.IdCliente,
           c.NombreCompleto,
           c.Telefono,
           c.IdPais,
           p.Nombre AS NombrePais,
           COUNT_BIG(1) OVER () AS TotalRegistros,
           @SafePageNumber AS PageNumber,
           @SafePageSize AS PageSize
    FROM dbo.Clientes AS c
        INNER JOIN dbo.Paises AS p
            ON p.IdPais = c.IdPais
    ORDER BY c.IdCliente OFFSET @Offset ROWS FETCH NEXT @SafePageSize ROWS ONLY;
END;
GO
