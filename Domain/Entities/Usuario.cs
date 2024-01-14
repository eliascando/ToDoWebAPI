namespace Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set;}

        public byte[]? Foto { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }

        // Propiedad de navegación para relacionar con Nota
        public ICollection<Nota> Notas { get; set; } = new List<Nota>();
    }
}
