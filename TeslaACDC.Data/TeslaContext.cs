using System;
using Microsoft.EntityFrameworkCore;
using TeslaACDC.Data.Models;

namespace TeslaACDC.Data;

public class TeslaContext : DbContext
{
    public TeslaContext(DbContextOptions<TeslaContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Artist> Artist { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        if (builder == null)
        {
            return;
        }

        builder.Entity<Album>().ToTable("album").HasKey(k => k.Id);
        builder.Entity<Artist>().ToTable("artist").HasKey(k => k.Id);
    }
}
