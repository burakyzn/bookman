using System;
using System.Collections.Generic;
using System.Text;
using BookstoreProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookstoreProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserDetails>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Kitap> Kitaplar { get; set; }

        public DbSet<Kategori> Kategoriler { get; set; }

        public DbSet<Dil> Diller { get; set; }
    }
}
