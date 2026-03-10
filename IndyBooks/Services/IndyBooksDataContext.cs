using IndyBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace IndyBooks.Services;

    public class IndyBooksDataContext:DbContext
    {
        public IndyBooksDataContext(DbContextOptions<IndyBooksDataContext> options) : base(options)
        {}

        //Define DbSets for Collections representing DB tables
        public DbSet<Book> Books { get; set; }
        public DbSet<Writer> Writers { get; set; }

        // Used to fine tune certain aspects of the Data model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //This code makes sure each Book always includes its Writer information
            modelBuilder.Entity<Book>()
                .Navigation(b=>b.Author)
                .AutoInclude();

        }
    }

