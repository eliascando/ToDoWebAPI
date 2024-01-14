namespace Domain.Interfaces
{
    public interface IEliminar<TId>
    {
        Boolean Eliminar(TId id);
    }
}
