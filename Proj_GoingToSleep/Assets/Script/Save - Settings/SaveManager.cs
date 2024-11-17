using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    [Header("\u2014\u2014 Information variables \u2014\u2014")]
    [SerializeField] SettingsSO_Script settings_SO;
    [SerializeField] UsefulThingsSO_Script things_SO;

    [Space(20)]
    [SerializeField] string fileName = "unityutilityasset";
    string file_path;

    const string HIGHSCORE_TITLE = "# HIGH SCORE #",
                 SETTINGS_TITLE = "# SETTINGS #";
    
    
    
    private void Awake()
    {
        //Takes the file path
        file_path = Application.dataPath + "/" + fileName + ".txt";
    }



    [ContextMenu("\u2013 Save in a file \u2013")]
    public void SaveGame()
    {
        string saveString = "";


        #region -- High Score --

        saveString += HIGHSCORE_TITLE + "\n";

        saveString += things_SO.GetHighScore() + "\n";   //Adds the high score

        #endregion


        #region -- Settings --

        saveString += "\n" + SETTINGS_TITLE + "\n";

        //Adds all the chosen Settings
        saveString += (int)settings_SO.GetChosenLanguage() + "\n";
        saveString += settings_SO.GetMusicVolume_Percent() + "\n";
        saveString += settings_SO.GetSoundVolume_Percent() + "\n";
        saveString += settings_SO.GetIsEndDingOn() + "\n";
        saveString += settings_SO.GetEndDingVolume_Percent() + "\n";
        saveString += settings_SO.GetIsBaahOn() + "\n";
        saveString += settings_SO.GetIsFullscreen() + "\n";

        #endregion


        //Overwrites the file
        //(if it doesn't exist, it creates a new one and writes on it)
        File.WriteAllText(file_path, saveString);



        #region Final save file
        //  0:  ### HIGH SCORE ###
        //  1:  High Score
        //  2:  
        //  3:  ### SETTINGS ###
        //  4:  Language
        //  5:  Music volume
        //  6:  Sound volume
        //  7:  Is End Ding On
        //  8:  End Ding Volume
        //  9:  Is Baah On
        // 10:  Is Fullscreen
        // 11:  
        // 12:  
        #endregion
    }


    [ContextMenu("\u2013 Load the file \u2013")]
    public void LoadGame()
    {
        string[] fileReading = new string[0];

        int i_highScore = 0,
            i_settings = 0;


        //Read the save file
        if (File.Exists(file_path))
        {
            fileReading = File.ReadAllLines(file_path);
        }
        else
        {
            print("[!] Error message");
            return;
        }

        #region Finding the starts points

        //Search in the array the start points of the various "regions"
        for (int i = 0; i < fileReading.Length; i++)
        {
            switch (fileReading[i])
            {
                case HIGHSCORE_TITLE:
                    i_highScore = i;
                    break;

                case SETTINGS_TITLE:
                    i_settings = i;
                    break;
            }
        }

        #endregion


        #region -- High Score --

        //Turns from string to int
        int highScore_load = int.Parse(fileReading[i_highScore + 1]);

        //Loads the high score number
        things_SO.LoadHighScore(highScore_load);

        #endregion


        #region -- Settings --

        //Turns from string to int
        int language_load = int.Parse(fileReading[i_settings + 1]);
        float musicVol_load = float.Parse(fileReading[i_settings + 2]),
              soundVol_load = float.Parse(fileReading[i_settings + 3]),
              endDingVol_load = float.Parse(fileReading[i_settings + 5]);
        bool isEndDingOn_load = bool.Parse(fileReading[i_settings + 4]),
              isBaahOn_load = bool.Parse(fileReading[i_settings + 6]),
              fullscreen_load = bool.Parse(fileReading[i_settings + 7]);

        //Loads all settings numbers
        settings_SO.ChangeLanguage(language_load);
        settings_SO.ChangeMusicVolume(musicVol_load);
        settings_SO.ChangeSoundVolume(soundVol_load);
        settings_SO.ChangeIsEndDingOn(isEndDingOn_load);
        settings_SO.ChangeEndDingVolume(endDingVol_load);
        settings_SO.ChangeIsBaahOn(isBaahOn_load);
        settings_SO.ToggleFullscreen(fullscreen_load);

        #endregion
    }


    public void GenerateNewGame()
    {
        //Deletes the save file
        DeleteSaveFile();


        //Resets the high score number
        things_SO.LoadHighScore(0);


        //Saves the game in a new file
        SaveGame();
    }

    public void DeleteSaveFile()
    {
        //If it exists, deletes it
        if (File.Exists(file_path))
            File.Delete(file_path);
    }


    #region Useful Functions
    
    
    #endregion
}
