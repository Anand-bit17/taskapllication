using emptyapp.Models.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace emptyapp.Data
{
    public class MvcdemoDBContext : DbContext

    {
        public MvcdemoDBContext(DbContextOptions Options): base(Options)
            {

            }
        public DbSet<employee> employees { get; set; }
    }
}