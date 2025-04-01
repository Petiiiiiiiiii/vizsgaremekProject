using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dungeonMaster_db.Models;

public partial class UnlockedSkill
{
    [Key]
    [Column("PlayerID")]
    public int PlayerId { get; set; }

    public int? Skill { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UnlockDate { get; set; }

    [ForeignKey("PlayerId")]
    [InverseProperty("UnlockedSkill")]
    public virtual Player Player { get; set; } = null!;
}
