using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/UsefulThingsSO_Script (S.O.)", fileName = "UsefulThings_SO")]
public class UsefulThingsSO_Script : ScriptableObject
{
    #region Things to save

    [Header("\u2014 Things to save \u2014")]
    [SerializeField] int highScore;


    public int GetHighScore() => highScore;


    public void LoadHighScore(int new_highScore)
    {
        highScore = new_highScore;
    }

    public void ResetHighScore()
    {
        highScore = 0;
    }

    #endregion
    

    #region Sleepiness range
    
    [Header("\u2014 Sleepiness range \u2014")]
    [Range(-10, 10)]
    [SerializeField] float sleepiness;
    
    
    public float GetSleepiness() => sleepiness;


    public void ChangeSleepiness_Add(float valToAdd)
    {
        sleepiness += valToAdd;
    }

    public void ChangeSleepiness_Mult(float mult)
    {
        sleepiness *= mult;
    }
    
    #endregion
}
