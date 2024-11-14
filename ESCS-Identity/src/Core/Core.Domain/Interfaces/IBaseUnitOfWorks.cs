namespace Core.Domain.Interfaces
{
    public interface IBaseUnitOfWorks : IDisposable
    {

        Task SaveChangesAsync();
    }
}
