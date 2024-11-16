using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;

[CreateAssetMenu(menuName = "Scriptable Objects/Settings (S.O.)", fileName = "Settings_SO")]
public class SettingsSO_Script : ScriptableObject
{
	//Main Menu
	#region Exit / Quit

	public void ExitGame()
	{
		Application.Quit();
	}

	#endregion


	//Settings
	#region Language Selection

	[Space(15)]
	[SerializeField] Language_Enum chosenLanguage;

	public void ChangeLanguage(Language_Enum l)
	{
		chosenLanguage = l;
	}
	public void ChangeLanguage(int i)
	{
		chosenLanguage = (Language_Enum)i;
	}

	public Language_Enum GetChosenLanguage() => chosenLanguage;

	#endregion


	#region Volume and Audio

	[Space(15)]
	[SerializeField] AudioMixer generalMixer;
	#region Variable functions (with RMB)
	[ContextMenuItem("\u2013 Reset to default settings \u2013", nameof(ResetAudioCurve))]
	#endregion
	[SerializeField] AnimationCurve audioCurve;
	[Range(0, 110)]
	[SerializeField] float musicVolume = 0f;
	[Range(0, 110)]
	[SerializeField] float soundVolume = 0f;
	
	[Space(15)]
	[SerializeField] bool isEndDingOn = true;
	[Range(0.1f, 1)]
	[SerializeField] float endDingVolume = 0.1f;
	[SerializeField] bool isBaahOn = false;

	///<summary></summary>
	/// <param name="vM"> new volume, in range [0; 1.1]</param>
	public void ChangeMusicVolume(float vM)
	{
		//Puts as volume in the mixer between [-80; 5] dB
		generalMixer.SetFloat("musVol", audioCurve.Evaluate(vM));

		musicVolume = vM * 100;
	}
	///<summary></summary>
	/// <param name="vS"> new volume, in range [0; 1.1]</param>
	public void ChangeSoundVolume(float vS)
	{
		//Puts as volume in the mixer between [-80; 5] dB
		generalMixer.SetFloat("sfxVol", audioCurve.Evaluate(vS));

		soundVolume = vS * 100;
	}
	///<summary></summary>
	/// <param name="vED"> new volume, in range [0; 1.1]</param>
	public void ChangeEndDingVolume(float vED)
	{
		//Puts as volume in the mixer between [-80; 5] dB
		generalMixer.SetFloat("endDingVol", audioCurve.Evaluate(vED));

		endDingVolume = vED;
	}

	///<summary></summary>
	/// <param name="vM"> new volume, in range [0; 11]</param>
	public void ChangeMusicVolumeTen(float vM)
	{
		vM /= 10;

		//Puts as volume in the mixer between [-80; 5] dB
		generalMixer.SetFloat("musVol", audioCurve.Evaluate(vM));

		musicVolume = vM * 100;
	}
	///<summary></summary>
	/// <param name="vS"> new volume, in range [0; 11]</param>
	public void ChangeSoundVolumeTen(float vS)
	{
		vS /= 10;

		//Puts as volume in the mixer between [-80; 5] dB
		generalMixer.SetFloat("sfxVol", audioCurve.Evaluate(vS));

		soundVolume = vS * 100;
	}
	///<summary></summary>
	/// <param name="vED"> new volume, in range [0; 11]</param>
	public void ChangeEndDingVolumeTen(float vED)
	{
		vED /= 10;

		//Puts as volume in the mixer between [-80; 5] dB
		generalMixer.SetFloat("endDingVol", audioCurve.Evaluate(vED));

		endDingVolume = vED * 100;
	}

	public void ChangeIsEndDingOn(bool isOn)
	{
		isEndDingOn = isOn;
	}

	public void ChangeIsBaahOn(bool isOn)
	{
		isBaahOn = isOn;
	}

	public AnimationCurve GetVolumeCurve() => audioCurve;

	public float GetMusicVolume() => audioCurve.Evaluate(musicVolume);
	public float GetMusicVolume_Percent() => musicVolume / 100;
	public float GetSoundVolume() => audioCurve.Evaluate(soundVolume);
	public float GetSoundVolume_Percent() => soundVolume / 100;

	public bool GetIsEndDingOn() => isEndDingOn;
	public float GetEndDingVolume() => audioCurve.Evaluate(endDingVolume);
	public float GetEndDingVolume_Percent() => endDingVolume / 100;
	public bool GetIsBaahOn() => isBaahOn;



	public void ResetAudioCurve()
	{
		//Removes all keyframes (if there's any)
		if (audioCurve.keys.Length >= 0)
		{
			audioCurve.keys = null;
		}

		//Creates some new ones with parameters used by the audio
		Keyframe[] newKeys = new Keyframe[]
		{
			new Keyframe(0, -60,
						 0, 172.1225f),
			new Keyframe(1, 0),
			new Keyframe(1.1f, 5)
		};

		audioCurve.keys = newKeys;   //Adds them to the curve

		//Fixes the tangents for the second and third keyframe
		//to better "adjust" them to the audio's decibels
		AnimationUtility.SetKeyBroken(audioCurve, 1, true);
		AnimationUtility.SetKeyRightTangentMode(audioCurve, 1, AnimationUtility.TangentMode.Linear);
		AnimationUtility.SetKeyLeftTangentMode(audioCurve, 2, AnimationUtility.TangentMode.Linear);
	}

	#endregion


	#region Fullscreen

	[Space(15)]
	[SerializeField] bool fullscreen = true;

	public void ToggleFullscreen(bool yn)
	{
		Screen.fullScreen = yn;

		fullscreen = yn;
	}

	public bool GetIsFullscreen() => fullscreen;

	#endregion


	//Other
	#region Other functions

	//Languages' Enum
	public enum Language_Enum
	{
		English,
		Italian
	}

	//Reset all settings
	[ContextMenu("\u2013 Reset all settings to their default values \u2013")]
	void ResetAllSettings()
	{
		ChangeLanguage(0);
		
		ChangeMusicVolume(0.6f);
		ChangeSoundVolume(0.4f);
		
		ChangeIsEndDingOn(true);
		ChangeEndDingVolume(0.2f);
		ChangeIsBaahOn(false);
		
		ToggleFullscreen(true);
	}

	#endregion
}
