using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScoreList : MonoBehaviour 
{
	public GameObject playerScoreEntryPrefab;
	ScoreManager scoreManager;
	int lastChangeCounter;

	public void Start()
	{
		scoreManager = GameObject.FindObjectOfType<ScoreManager> ();
		lastChangeCounter = scoreManager.getChangeCounter ();

	}

	public void Update()
	{


		//if (scoreManager.getChangeCounter() == lastChangeCounter)
			//return;
		
		while (this.transform.childCount > 0) 
		{
			Transform c = this.transform.GetChild (0);
			c.SetParent (null);
			Destroy (c.gameObject);
		}

		string[] names = scoreManager.getPlayerNames ();
		foreach(string name in names) 
		{
			GameObject go = (GameObject)Instantiate (playerScoreEntryPrefab);
			go.transform.SetParent (this.transform, false);
			go.transform.Find("userName").GetComponent<Text>().text = name;
			go.transform.Find("Score").GetComponent<Text>().text = scoreManager.getScore(name).ToString();


		}
	}

}
