namespace Domain.Interfaces
{
    public interface IAuthentification<T, TDto>
    {
         T Login(TDto dto);
    }
}
