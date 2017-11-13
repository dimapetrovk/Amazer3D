using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Levels;

public class Field : MonoBehaviour
{
	public GameObject wallTemplate;
	public GameObject ground;
	public GameObject player;
	public GameObject finish;
	
	private Level level;
	private int levelNum;
	private float time;
	
	void Start ()
	{
		ReadParams();
		ReadLevel();
		DrawWalls();
		DrawCharacter();
	}
	
	void Update ()
	{
		UpdateTime();
	}

	void DrawCharacter()
	{
		if(level.character != null)
			player.transform.position = new Vector3(level.character[0], level.character[1], level.character[2]);
		else
			player.transform.position = new Vector3(0, player.transform.localScale.y / 2, 0);
	}

	void DrawWalls()
	{
		Vector3 sizeVertical = VerticalSize();
		Vector3 sizeHorizontal = HorizontalSize();
		for (int i = 0; i <= level.height; i++)
		{
			for (int j = 0; j <= 2 * level.width; j++)
			{
				if (level.maze[i, j] == '_')
				{
					GameObject wall = Instantiate(wallTemplate);
					wall.transform.localScale = sizeHorizontal;
					wall.transform.position = new Vector3((-90 + sizeHorizontal.x * (j + 1)) / 2,
						sizeHorizontal.y / 2,
						(-70 + sizeHorizontal.z) / 2 + sizeHorizontal.x * (i - 1));
				}
				else if (level.maze[i, j] == '|')
				{
					GameObject wall = Instantiate(wallTemplate);
					wall.transform.localScale = sizeVertical;
					wall.transform.position = new Vector3((-90 + sizeVertical.x + sizeVertical.z * (j + 1)) / 2,
						sizeHorizontal.y / 2,
						(-70 + sizeVertical.z) / 2 + sizeVertical.z * (i - 2));
				}
			}
		}
		finish.transform.position = new Vector3((-90 + sizeVertical.x + sizeVertical.z * (2 * level.width)) / 2,
			finish.transform.localScale.y / 2,
			(-70 + sizeVertical.z) / 2 + sizeVertical.z * (level.height - 2));
		Destroy(wallTemplate);
	}

	Vector3 HorizontalSize()
	{
		Vector3 size = new Vector3(0, 0.5f, 0);
		size.x = ground.transform.localScale.x / level.width;
		size.z = size.x * 0.1f;
		return size * 8;
	}

	Vector3 VerticalSize()
	{
		Vector3 size = new Vector3(0, 0.5f, 0);
		size.z = ground.transform.localScale.z / level.height;
		size.x = size.z * 0.1f;
		return size * 8;
	}

	void ReadParams()
	{
		if (Scenes.GetSceneParameters() != null)
			levelNum = Convert.ToInt32(Scenes.GetParam("levelNum"));
		else
			levelNum = 0;
	}

	void ReadLevel()
	{
		level = LevelService.Read(levelNum);
		if (level.maze == null)
		{
			level.maze = new MazeGenerator(level.width, level.height, levelNum).Generate();
			SaveLevel();
		}
	}

	void SaveLevel()
	{
		level.character = new[] {player.transform.position.x, player.transform.position.y, player.transform.position.z};
		LevelService.Save(level, levelNum);
	}

	void UpdateTime()
	{
		level.time += Time.deltaTime;
	}

	public void Back()
	{
		SaveLevel();
		NextScene("Menu");
	}
	
	public void Replay()
	{
		time = level.time;
		level.Clear();
		SaveLevel();
		NextScene("Field");
	}
	
	public void Finish()
	{
		time = level.time;
		level.Clear();
		SaveLevel();
		NextScene("WinScreen");
	}

	public void NextScene(string scene)
	{
		Dictionary<string, object> parameters = new Dictionary<string, object>();
		parameters.Add("levelNum", levelNum.ToString());
		parameters.Add("time", time.ToString());
		Scenes.Load(scene, parameters);
	}
}
