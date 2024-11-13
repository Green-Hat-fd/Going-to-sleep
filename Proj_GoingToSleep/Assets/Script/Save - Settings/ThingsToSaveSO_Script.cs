using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ThingsToSaveSO_Script (S.O.)", fileName = "ThingsToSaveSO_Script_SO")]
public class ThingsToSaveSO_Script : ScriptableObject
{
    [Space(15)]
    [SerializeField] int highScore;
}
