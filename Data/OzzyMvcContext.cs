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

        public DbSet<ozzy_mvc.Models.Student> Student { get; set; }

        public DbSet<ozzy_mvc.Models.Booking> Booking { get; set; }
    }
}