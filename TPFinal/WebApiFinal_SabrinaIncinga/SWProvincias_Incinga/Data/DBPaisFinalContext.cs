using Microsoft.EntityFrameworkCore;
using SWProvincias_Incinga.Models;

namespace SWProvincias_Incinga.Data
{
    public class DBPaisFinalContext : DbContext
    {
        public DBPaisFinalContext(DbContextOptions<DBPaisFinalContext> options) : base(options)
        {
        }

        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
    }
}
