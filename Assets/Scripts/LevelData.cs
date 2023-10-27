using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class LevelData
{
    // Variable declartions.
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

    // Update level method.
    public void UpdateLevelData(int time, int tokens, int total, int deaths)
    {
        level = GameData.currentLevel;

        // For the values below, we want to make sure we are only updating values if they're better than our previous score.
        if (this.time < time)
        {
            this.time = time;
        }
        if (this.tokens < tokens)
        {
            this.tokens = tokens;
        }
        if (this.total < total)
        {
            this.total = total;
        }
        if (this.deaths < deaths)
        {
            this.deaths = deaths;
        }
    }

    // Just a method to Debug since the ToString method Unity uses is hideous to me.
    override public string ToString()
    {
        return "Level: " + level +
                "\nTime: " + time + 
                "\nItems: " + tokens + 
                "\nTotal: " + total + 
                "\nDeaths: " + deaths;
    }
}
