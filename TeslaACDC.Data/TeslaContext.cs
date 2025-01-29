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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        if (builder == null)
        {
            return;
        }

        builder.Entity<Album>().ToTable("Album").HasKey(k => k.Id);
        builder.Entity<Artist>().ToTable("Artist").HasKey(k => k.Id);
        base.OnModelCreating(builder);
    }
}
