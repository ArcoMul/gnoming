using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{
	public enum AngryLevels { Good, Normal, Bad, Worse}

	Canvas ActiveCanvas;

	int CanvasCount;

	public House[] Houses;

	void Start ()
	{
		Houses[0].Request(Item.Types.Drill);
		Houses[1].Offer(Item.Types.Drill);
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

	void SetActiveCanvas (Canvas c)
	{
		if (ActiveCanvas != null)
			ActiveCanvas.Disable();

		c.Enable();
		ActiveCanvas = c;
	}

}
