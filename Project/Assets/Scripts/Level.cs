using UnityEngine;
using System;
using System.Collections;

public class Level : MonoBehaviour
{
	public enum AngryLevels { Good, Normal, Bad, Worse}

	Canvas ActiveCanvas;

	int CanvasCount;

	public House[] Houses;

	private int _Score = 0;
	public int Score {
		get {
			return _Score;
		}
		set {
			if (value != _Score) {
				_Score = value;
				OnScoreChange(_Score);
			}
		}
	}

	public delegate void OnScoreChangeEventHandler (int Score);
	public event OnScoreChangeEventHandler ScoreChange;
	public virtual void OnScoreChange (int Score)
	{
		if (ScoreChange != null) {
			ScoreChange(Score);
		}
	}

	void Start ()
	{
		InvokeRepeating ("SpawnProduct", 2, 10);

		foreach (House house in Houses) {
			house.Score += OnScore;
		}

	}
	
	void Update ()
	{

	}

	void OnScore ()
	{
		Score++;
	}

	public void Lose ()
	{
		GameObject o = (GameObject) Instantiate (Resources.Load ("Lose"));
		o.transform.FindChild ("Score").GetComponent<TextMesh>().text = "Score: " + Score + " deals!";
		Invoke ("GoBackToStart", 3);
	}

	void GoBackToStart ()
	{
		Application.LoadLevel("Start");
	}

	void SetActiveCanvas (Canvas c)
	{
		if (ActiveCanvas != null)
			ActiveCanvas.Disable();

		c.Enable();
		ActiveCanvas = c;
	}

	void SpawnProduct ()
	{
		Array values = Enum.GetValues(typeof(Item.Types));
		System.Random random = new System.Random();
		Item.Types RandomType = (Item.Types)values.GetValue(random.Next(values.Length));

		if (!RandomRequest (RandomType)) {
			CancelInvoke ();
			Debug.Log ("LOSE!");
			Lose();
		}
		if (!RandomOffer (RandomType)) {
			CancelInvoke ();
			Debug.Log ("LOSE!");
			Lose();
		}
	}

	private int start = -1;
	bool RandomRequest (Item.Types Type, int n = -1)
	{
		Debug.Log ("Random Request");

		if (start >= 0 && n == start) {
			return false;
		}

		if (n == -1) {
			n = (int) Mathf.Floor (UnityEngine.Random.value * Houses.Length);
			start = n;
		}

		n = n >= Houses.Length ? 0 : n;

		if (!Houses[n].Request(Type)) {
			return RandomRequest (Type, n + 1);
		} else {
			start = -1;
			return true;
		}
	}

	bool RandomOffer (Item.Types Type, int n = -1)
	{
		Debug.Log ("Random Offer");

		if (start >= 0 && n == start) {
			return false;
		}

		if (n == -1) {
			n = (int) Mathf.Floor (UnityEngine.Random.value * Houses.Length);
			start = n;
		}
		
		n = n >= Houses.Length ? 0 : n;
		
		if (!Houses[n].Offer(Type)) {
			return RandomOffer (Type, n + 1);
		} else {
			start = -1;
			return true;
		}
	}
		              
}
