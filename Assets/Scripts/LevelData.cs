using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class LevelData
{
    private int level;
    private int time;
    private int tokens;
    private int total;
    private int deaths;

    // Empty constructor used when starting a level (aside from Level 1) to create new data as needed to save on space.
    public LevelData() 
    {
        level = GameData.currentLevel;
        this.time = 0;
        this.tokens = 0;
        this.total = 0;
        this.deaths = 0;
    }

    public void UpdateLevelData(int time, int tokens, int total, int deaths)
    {
        level = GameData.currentLevel;
        this.time = time;
        this.tokens = tokens;
        this.total = total;
        this.deaths = deaths;
    }

    override public string ToString()
    {
        return "Level: " + level +
                "\nTime: " + time + 
                "\nItems: " + tokens + 
                "\nTotal: " + total + 
                "\nDeaths: " + deaths;
    }
}
