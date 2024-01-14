using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AuthResponseDTO
    {
        public Guid Id { get; set; }
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public string Token { get; set; }
    }
}
