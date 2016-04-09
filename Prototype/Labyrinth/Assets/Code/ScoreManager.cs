using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ScoreManager : MonoBehaviour
{

	private Dictionary<string, int> scoreBoard;

	int changeCounter = 0;
	public void Start()
	{
		setScore ("AAD", 9001);
		setScore ("CRF", 10000);
		setScore ("AAA", 8000);
		setScore ("BBB", 8001);
		setScore ("CCC", 100);
		DontDestroyOnLoad (transform.gameObject);

		//Debug.Log (getScore("xer096"));
	}

	private void init()
	{
		if (scoreBoard != null)
			return;
		scoreBoard = new Dictionary<string, int> ();
	}

	public int getScore(string userName)
	{
		init ();

		if (scoreBoard.ContainsKey (userName) == false) 
		{
			return 0;
		}

		return scoreBoard [userName];
	}

	public void setScore(string userName, int score)
	{
		init ();
		changeCounter++;
		if (scoreBoard.ContainsKey (userName) == false)
			scoreBoard.Add(userName, score);
		else 
			scoreBoard [userName] = score;

		Debug.Log ("Score set Name: " + userName + " Score: " + score);
	}

	public string[] getPlayerNames()
	{
		init ();

		return scoreBoard.Keys.OrderByDescending(n => getScore(n)).ToArray();
	}

	public int getChangeCounter()
	{
		return changeCounter;
	}

	public bool checkScore(int points)
	{
		init ();
		var isGreater = false;
		string[] names = getPlayerNames ();
		foreach (string name in names) 
		{
			isGreater = getScore (name) < points;
		}

		return isGreater;
	}

}
