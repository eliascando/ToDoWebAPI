namespace Domain.Interfaces.Repository
{
    public interface IRepositoryBase<T, TId>
        : IListar<T, TId>,IAgregar<T>, IEliminar<TId>, IActualizar<T>
    {

    }
}
