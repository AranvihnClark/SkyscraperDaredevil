using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class LevelData
{
    private int level;
    private int time;
    private int items;
    private int score;
    private int total;
    private int deaths;

    // Empty constructor used when starting a level (aside from Level 1) to create new data as needed to save on space.
    public LevelData() 
    {
        // Increase our game data's current level by 1.
        GameData.currentLevel++;
    }

    // Full constructor to be updated at the end of a level.
    public LevelData(int time, int items, int score, int total, int deaths)
    {
        level = GameData.currentLevel;
        this.time = time;
        this.items = items;
        this.score = score;
        this.total = total;
        this.deaths = deaths;
    }

    public void UpdateLevelData(int level, int time, int items, int score, int total, int deaths)
    {
        this.level = level;
        this.time = time;
        this.items = items;
        this.score = score;
        this.total = total;
        this.deaths = deaths;
    }
}
