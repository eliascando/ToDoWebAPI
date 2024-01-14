namespace Domain.Interfaces
{
    public interface IAuthorization<T>
    {
        string GenerateToken(T user);
    }
}
