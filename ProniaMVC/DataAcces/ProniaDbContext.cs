using Microsoft.EntityFrameworkCore;
using ProniaMVC.Models;

namespace ProniaMVC.DataAcces
{
    public class ProniaDbContext : DbContext
    {
        public DbSet<Slider> Sliders { get; set; }

        public ProniaDbContext(DbContextOptions opt) : base(opt) { }
    }
}
