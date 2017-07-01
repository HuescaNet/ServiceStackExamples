using System;

namespace Filtros
{
    interface IAuditable
    {
        string Usuario { get; set; }
        DateTime FechaCreacion { get; set; }
        DateTime FechaUltimaModificacion { get; set; }
    }
}