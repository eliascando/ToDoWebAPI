namespace Domain.Entities
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } // Pendiente, En Proceso, Terminado, Archivado

        // Propiedad de navegación para relacionar con Nota
        public ICollection<Nota> Notas { get; set; }
    }
}
