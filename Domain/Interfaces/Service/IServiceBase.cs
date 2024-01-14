namespace Domain.Interfaces.Service
{
    public interface IServiceBase<T, TId> 
        : IListar<T, TId>,IActualizar<T>, IAgregar<T>, IEliminar<TId>
    {

    }
}
