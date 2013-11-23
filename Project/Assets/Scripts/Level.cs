using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{
	public enum AngryLevels { Good, Normal, Bad, Worse}

	Canvas ActiveCanvas;

	int CanvasCount;

	public House[] Houses;

	private int _Score;
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
		foreach (House house in Houses) {
			house.Score += OnScore;
		}

		Houses[0].Request(Item.Types.Pliers);
		Houses[1].Offer(Item.Types.Pliers);
	}
	
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
//			Debug.Log ("New one!");
//
//			GameObject canvas = ((GameObject) Instantiate (Resources.Load ("Canvas")));
//			canvas.transform.position = new Vector3 (0,0, (float) -CanvasCount / 100);
//			Canvas c = canvas.GetComponent<Canvas>();
//			SetActiveCanvas(c);
//			CanvasCount++;
//
//			GameObject Gnome = ((GameObject) Instantiate (Resources.Load ("Gnome"), new Vector3 (99,99,99), new Quaternion (0,0,0,0)));
//			Gnome g = Gnome.GetComponent<Gnome>();
//			g.AttachCanvas (c);
		}
	}

	void OnScore ()
	{
		Score++;
	}

	void SetActiveCanvas (Canvas c)
	{
		if (ActiveCanvas != null)
			ActiveCanvas.Disable();

		c.Enable();
		ActiveCanvas = c;
	}

}
