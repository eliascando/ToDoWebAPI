namespace Domain.Interfaces
{
    public interface INota<T>
    {
        List<T> ObtenerNotasDeUsuario(Guid id);

        void ActualizarEstadoNota(Guid id, int estado);
    }
}
