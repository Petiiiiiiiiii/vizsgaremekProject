using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class PlayerModel 
{
    public int playerId { get; set; }
    public string username { get; set; } 
    public string passwordHash { get; set; }
    public string email { get; set; }
    public int level { get; set; }
    public int sp { get; set; }
    public int permission { get; set; }
    public DateTime regDate { get; set; }
    public List<MatchlogModel> matchLogs { get; set; } 
    public List<UnlockedSkillModel> unlockedSkills { get; set; } 
}

public class MatchlogModel 
{
    public int MatchId { get; set; }
    public int PlayerId { get; set; }
    public int Kills { get; set; }
    public int MatchDuration { get; set; }
    public bool Win { get; set; }
}
public class UnlockedSkillModel
{
    public int UnlockedSkillId { get; set; }
    public int PlayerId { get; set; }
    public int? Skill { get; set; }
    public DateTime UnlockDate { get; set; }
}
