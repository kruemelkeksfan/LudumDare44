using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidAnimator : MonoBehaviour
	{
	private const float STAGE_TIME = 0.5f;

	private GameObject pyramid = null;
	private List<GameObject> pyramidstages = null;
	private int currentstage = 1;
	private float passedtime = 0.0f;
    public AudioSource audioSource;

	void Start()
		{
		pyramid = GameObject.Find("Pyramid");

		pyramidstages = new List<GameObject>();
		foreach(Transform stage in pyramid.transform)
			{
			if(stage.gameObject.name != "PyramidFoundation")
				{
				stage.gameObject.SetActive(false);
				}
			pyramidstages.Add(stage.gameObject);
			}
		}

	void Update()
		{
		passedtime += Time.deltaTime;

		if(passedtime > STAGE_TIME && currentstage < pyramidstages.Count)
			{
			pyramidstages[currentstage].SetActive(true);
			++currentstage;
            audioSource.Play();
            passedtime = 0.0f;
			}
		}
	}
