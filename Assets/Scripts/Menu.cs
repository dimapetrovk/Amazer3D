using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GoToLevel(int levelNum)
	{
		Dictionary<string, object> parameters = new Dictionary<string, object>();
		parameters.Add("levelNum", levelNum.ToString());
		Scenes.Load("Field", parameters);
	}
}
