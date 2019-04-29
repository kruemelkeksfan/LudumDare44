using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
	{
	private GameObject[] pages = null;

	void Start()
		{
		pages = new GameObject[3];
		pages[0] = GameObject.Find("General");
		pages[1] = GameObject.Find("Management");
		pages[1].SetActive(false);
		pages[2] = GameObject.Find("Combat");
		pages[2].SetActive(false);
		}

	public void nextPage(int i)
		{
		foreach(GameObject page in pages)
			{
			page.SetActive(false);
			}

		if(i >= 0 && i < 3)
			{
			pages[i].SetActive(true);
			}
		}
	}
