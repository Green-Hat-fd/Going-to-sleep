using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ThingsToSaveSO_Script (S.O.)", fileName = "ThingsToSaveSO_Script_SO")]
public class ThingsToSaveSO_Script : ScriptableObject
{
	[Space(15)]
	[SerializeField] int highScore;
	
	[Space(15), Range(-10, 10)]
	[SerializeField] float sleepiness;


	public int GetHighScore() => highScore;
	public int GetSleepiness() => sleepiness;


	public void LoadHighScore(int new_highScore)
	{
		highScore = new_highScore;
	}
}
