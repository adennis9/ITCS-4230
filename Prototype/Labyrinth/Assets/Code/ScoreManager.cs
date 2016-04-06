using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour 
{
	private Dictionary<string, int> scoreBoard;

	public void Start()
	{
		setScore ("xer096", 9001);

		Debug.Log (getScore("xer096"));
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

		if (scoreBoard.ContainsKey (userName) == false)
			scoreBoard.Add(userName, score);
		else 
			scoreBoard [userName] = score;
	}


}
