using UnityEngine;
using System.Collections;

public class LevelCamera : MonoBehaviour {

	public TextMesh ScoreLabel;

	// Use this for initialization
	void Start () {
		Level level = GameObject.Find ("Level").GetComponent<Level>();
		level.ScoreChange += OnScoreChange;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnScoreChange (int Score)
	{
		ScoreLabel.text = "x" + Score;
	}
}
