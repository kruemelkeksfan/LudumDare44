using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
	{
	public List<GameObject> levels = null;
	public List<GameObject> options = null;

	public Slider cameraslider = null;
	public Slider musicslider = null;
	public Slider sfxslider = null;

	private float cameraspeed = 1.0f;
	private float musicvolume = 1.0f;
	private float sfxvolume = 1.0f;

	private void Start()
		{
		DontDestroyOnLoad(this);
		}

	public void toggleLevels(bool onoff)
		{
		foreach(GameObject level in levels)
			{
			level.SetActive(onoff);
			}
		}

	public void toggleOptions(bool onoff)
		{
		foreach(GameObject option in options)
			{
			option.SetActive(onoff);
			}
		}

	public void updateSettings()
		{
		cameraspeed = cameraslider.value;
		musicvolume = musicslider.value;
		sfxvolume = sfxslider.value;
		}

	public void startLevel(string levelname)
		{
		SceneManager.LoadScene(levelname, LoadSceneMode.Single);
		}

	public void quitGame()
		{
		Application.Quit();
		}
	}
