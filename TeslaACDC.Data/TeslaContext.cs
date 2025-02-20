using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeslaACDC.Data.Models;

namespace TeslaACDC.Data;

public class TeslaContext : IdentityDbContext<ApplicationUser>
{
    public TeslaContext(DbContextOptions<TeslaContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Artist> Artist { get; set; }
    public DbSet<Song> Songs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        if (builder == null)
        {
            return;
        }

        builder.Entity<Album>().ToTable("album").HasKey(k => k.Id);
        builder.Entity<Artist>().ToTable("artist").HasKey(k => k.Id);
        builder.Entity<Song>().ToTable("songs").HasKey(k => k.Id);
        base.OnModelCreating(builder);
    }
}
