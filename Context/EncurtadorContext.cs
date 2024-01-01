
using Encurtador.Models;
using Microsoft.EntityFrameworkCore;

namespace Encurtador.Context
{
    public class EncurtadorContext : DbContext
    {
        public DbSet<Link> Links { get; set; }
        public EncurtadorContext(DbContextOptions<EncurtadorContext> options) :base(options)
        {}
    }
}
