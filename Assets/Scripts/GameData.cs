using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public static class GameData
{
    // Global Game Data variables to be manipulated when loading the game up.
    public static int currentLevel;
    public static int levelsUnlocked;
    public static int tokensObtained;
    public static int tokensAvailable;
    public static int totalDeaths;
    public static int deaths;
    public static bool isLoading;
    public static List<LevelData> levels = new List<LevelData>();

    public static void Begin()
    {
        // This is currently being used as is because there is no 'start menu' and the player jumps into the game.
        // When such a menu is created, this will only be used when starting a 'new game' probably.
        // I feel like this will need to be changed later but for now, it is fine.
        currentLevel = 1;
        levelsUnlocked = currentLevel;
        tokensObtained = 0;
        tokensAvailable = tokensObtained;
        totalDeaths = 0;
        deaths = 0;
        levels.Add(new LevelData());
        isLoading = true;
    }

    // Updates our user's level data when they beat a level, basically.
    public static void UpdateLevel(LevelData level, int time, int tokens, int total, int deaths)
    {
        level.UpdateLevelData(time, tokens, total, deaths);
        tokensObtained += tokens;
        tokensAvailable += tokens;
        totalDeaths += deaths;
    }
    
    // Adds a new level to our user's level data when they start a level they haven't been to before.
    public static void NewLevel()
    {
        // Increase our game data's current level by 1 when we move to the next level.
        currentLevel++;
        deaths = 0;
        levels.Add(new LevelData());
    }
}
