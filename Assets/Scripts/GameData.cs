using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public static class GameData
{
    // Global Game Data to be manipulated when loading the game up.
    public static int currentLevel = 1;
    public static int deaths = 0;
    public static int totalDeaths = 0;
    public static List<LevelData> levels = new List<LevelData>();

    public static void Begin()
    {
        levels.Add(new LevelData());
    }

    public static void UpdateLevel(LevelData level, int time, int items, int total, int deaths)
    {
        level.UpdateLevelData(time, items, total, deaths);
    }
    
    public static void NewLevel()
    {
        // Increase our game data's current level by 1 when we move to the next level.
        currentLevel++;
        deaths = 0;
        levels.Add(new LevelData());
    }
}
