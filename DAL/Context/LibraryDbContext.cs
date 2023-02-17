using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        { }
    }
}
