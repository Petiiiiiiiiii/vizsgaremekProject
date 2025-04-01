using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dungeonMaster_db.Models;

public partial class MatchLog
{
    [Key]
    [Column("MatchID")]
    public int MatchId { get; set; }

    [Column("PlayerID")]
    public int PlayerId { get; set; }

    public int? Kills { get; set; }

    public int MatchDuration { get; set; }

    public bool Win { get; set; }

    [ForeignKey("PlayerId")]
    [InverseProperty("MatchLogs")]
    public virtual Player Player { get; set; } = null!;
}
