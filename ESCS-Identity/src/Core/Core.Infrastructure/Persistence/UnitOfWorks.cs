using Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistence
{
    public class UnitOfWorks : IBaseUnitOfWorks
    {
        private readonly DbContext _context;


        public UnitOfWorks(DbContext context, IServiceProvider provider)
        {
            _context = context;

        }



        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            try
            {


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
