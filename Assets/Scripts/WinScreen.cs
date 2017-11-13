using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class WinScreen : MonoBehaviour
{
	public Text text;
	
	private string time;
	
	// Use this for initialization
	void Start () {
		if (Scenes.GetSceneParameters() != null)
			time = Convert.ToString(Scenes.GetParam("time"));
		else
			time = "";
		text.text += time;
	}

	public void Menu()
	{
		Scenes.Load("Menu");
	}
}
