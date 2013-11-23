using UnityEngine;
using System.Collections;

public class Gnome : MonoBehaviour 
{

	Canvas canvas;

	int step = 0;

	private GameObject Img;

	private Animator anim;

	public bool Jumping = false;

	// product carried by gnome
	public GameObject Item;

	// has product?
	public bool hasItem = false; 

	void Start ()
	{
		Img = transform.FindChild ("Img").gameObject;
		anim = Img.GetComponent<Animator>();
		anim.SetTrigger ("Idle");
	}

	/**
	 * 
	 *  GNome actions
	 * 
	 */
	public void carryItem( Item item) 
	{

	}

	public void releaseItem( Item item)
	{

	}



	/*
	 * 
	 * 
	 */
	void Update ()
	{
		Walk ();
	}

	public void StartLineDraw ()
	{
		Debug.Log ("New one!");

		if (canvas != null) {
			Destroy (canvas.gameObject);
		} else {
			anim.SetTrigger ("Jump");
			Invoke ("StopJumping", 1.071f);
			Jumping = true;
		}

		GameObject obj = ((GameObject) Instantiate (Resources.Load ("Canvas")));
		obj.transform.position = new Vector3 (0,0, (float) -Canvas.CanvasCount / 100);
		Canvas c = obj.GetComponent<Canvas>();
		Canvas.CanvasCount++;
		step = 0;
		
		AttachCanvas (c);
	}

	void StopJumping ()
	{
		Jumping = false;
	}

	public void AttachCanvas (Canvas c)
	{
		canvas = c;
	}

	void Walk ()
	{
		if (canvas == null || step >= canvas.Points.Count) {
			if (canvas != null && step >= canvas.Points.Count) {
				anim.SetTrigger ("Idle");
			}
			return;
		}

		if (Jumping) return;

		anim.SetTrigger ("Walk");

		 Vector3 Pos = transform.position;
		 Vector3 Goal = canvas.GetPoint (step) + new Vector3 (0, 0, -1);
		 
		 if (Vector3.Distance (Pos, Goal) < 0.91f) {
			if (step > 1)
				canvas.RemoveStep (step - 2);

			if (step < 2)
				step++;
		}

		if (step > 0)
			canvas.SetStep (step - 1, new Vector3 (transform.position.x, transform.position.y, canvas.GetPoint(step - 1).z));

		 float Direction = Angle (Pos, Goal);

		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Direction));
		transform.position = transform.position - (transform.right * 0.015f);

		Img.transform.localRotation = Quaternion.Euler(new Vector3 (0,0, 360 - transform.rotation.eulerAngles.z));
	}

	public void CancelWalk ()
	{
		if (canvas != null)
			canvas.Clear ();
	}

	public void GiveProduct (Item.Types Type)
	{
		Item = (GameObject) Instantiate (Resources.Load ("Items/" + Type.ToString()));
		Item.transform.parent = Img.transform;
		Item.transform.localPosition = new Vector3 (0, 1.7f, 0);
		Item.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
	}

	public static float Angle (Vector3 p1, Vector3 p2)
	{
		// Direction vector. Line to follow to fly to the given object
		Vector3 direction = p1 - p2;
		
		// Direction vector to angle in degrees
		return 90 - (Mathf.Atan2 (direction.x, direction.y) * Mathf.Rad2Deg);
	}

}
