using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Nota
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Guid IdUsuario { get; set; }
        public int IdEstado { get; set; }

        // Propiedad de navegación
        public Estado Estado { get; set; }
        public Usuario Usuario { get; set; }

    }
}
