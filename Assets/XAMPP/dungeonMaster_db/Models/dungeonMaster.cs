using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dungeonMaster_db.Models;

public partial class dungeonMaster : DbContext
{
    public dungeonMaster()
    {
    }

    public dungeonMaster(DbContextOptions<dungeonMaster> options)
        : base(options)
    {
    }

    public virtual DbSet<MatchLog> MatchLogs { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<UnlockedSkill> UnlockedSkills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DungeonMaster;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MatchLog>(entity =>
        {
            entity.HasKey(e => e.MatchId).HasName("PK__MatchLog__4218C8378650A7FF");

            entity.Property(e => e.Kills).HasDefaultValue(0);

            entity.HasOne(d => d.Player).WithMany(p => p.MatchLogs).HasConstraintName("FK_MatchLogs_Players");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Players__4A4E74A815E3006F");

            entity.Property(e => e.RegDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<UnlockedSkill>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Unlocked__4A4E74A869013E86");

            entity.Property(e => e.PlayerId).ValueGeneratedNever();
            entity.Property(e => e.Skill).HasDefaultValue(0);
            entity.Property(e => e.UnlockDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Player).WithOne(p => p.UnlockedSkill).HasConstraintName("FK_UnlockedSkills_Players");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
