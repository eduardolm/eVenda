using Microsoft.EntityFrameworkCore;

namespace eVendas.Warehouse.Context
{
    public class DbInitializer
    {
        public static DbContextOptions<DbContext> DbOptions { get; set; }

        public static void Initialize(DbContext context)
        {
            DbOptions = new DbContextOptionsBuilder<DbContext>()
                .Options;

            context.Database.EnsureCreated();
        }
    }
}