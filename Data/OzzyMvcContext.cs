using Microsoft.EntityFrameworkCore;
using ozzy_mvc.Models;

namespace ozzy_mvc.Data
{
    public class OzzyMvcContext : DbContext
    {
        public OzzyMvcContext (DbContextOptions<OzzyMvcContext> options)
            : base(options)
        {
        }

        public DbSet<Equipment> Equipment { get; set; }
    }
}