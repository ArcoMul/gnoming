using UnityEngine;
using System.Collections;

public class Gnome : MonoBehaviour 
{
	//
	// Visual
	//
	private GameObject Img;
	private Animator anim;
	private bool Jumping = false;
	private GameObject VisualItem;
	private Canvas canvas;
	private int step = 0;
	public enum AnimStates {Idle, Front, Back, Left, Right};
	public AnimStates AnimState;

	//
	// Logic
	//
	private Item _currentItem = null;
	public Item CurrentItem {
		get{ return _currentItem;}
		set{ _currentItem = value; }
	}

	bool CanSwitchAnimation = true;

	void Start ()
	{
		Img = transform.FindChild ("Img").gameObject;
		anim = Img.GetComponent<Animator>();
		anim.SetTrigger ("Idle");
	}

	/**
	 * 
	 *  GNome actions
	 *  ANTON
	 * 
	 */
	public bool CarryItem( Item item) 
	{
		if (CurrentItem != null)
			return false;

		CurrentItem = item;
		DrawVisualItem ();
		return true;
	}

	public bool HasItem ()
	{
		return CurrentItem != null;
	}

	public Item GetItem ()
	{
		Item TempItem = CurrentItem;
		CurrentItem = null;
		DrawVisualItem ();
		return TempItem;
	}

	
	/*
	 * 
	 *  Visual Methods
	 *  ARCO!
	 */
	void Update ()
	{
		Walk ();
	}

	// Draw the current item
	private void DrawVisualItem()
	{
		if (CurrentItem != null)
		{
			VisualItem = (GameObject) Instantiate (Resources.Load ("Items/" + CurrentItem.Type.ToString()));
			VisualItem.transform.localScale = new Vector3 (0.8f, 0.8f, 1);
			VisualItem.transform.parent = Img.transform;
			VisualItem.transform.localPosition = new Vector3 (0, 1.7f, 0);
		}
		else if (VisualItem != null)
		{
			Destroy (VisualItem);
		}
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

	void SwitchAnimState (AnimStates State)
	{
		AnimState = State;
		if (State == AnimStates.Left || State == AnimStates.Left) { 
			anim.SetTrigger ("Side");
		} else {
			anim.SetTrigger (AnimState.ToString ());
		}
		CanSwitchAnimation = false;
		Invoke ("AllowSwitchAnimation", 0.3f);
	}

	void Walk ()
	{
		if (canvas == null || step >= canvas.Points.Count) {
			if (canvas != null && step >= canvas.Points.Count) {
				SwitchAnimState (AnimStates.Idle);
			}
			return;
		}

		if (Jumping) return;

		if (AnimState != AnimStates.Back && transform.rotation.eulerAngles.z <= 315 && transform.rotation.eulerAngles.z > 225 && CanSwitchAnimation) {
			SwitchAnimState (AnimStates.Back);
			Img.transform.localScale = new Vector3 (Mathf.Abs(Img.transform.localScale.x), Img.transform.localScale.y, Img.transform.localScale.z);
		} else if (AnimState != AnimStates.Left && (transform.rotation.eulerAngles.z <= 45 || transform.rotation.eulerAngles.z > 315) && CanSwitchAnimation) {
			SwitchAnimState (AnimStates.Left);
			Img.transform.localScale = new Vector3 (-Mathf.Abs(Img.transform.localScale.x), Img.transform.localScale.y, Img.transform.localScale.z);
		} else if (AnimState != AnimStates.Right && transform.rotation.eulerAngles.z <= 225 && transform.rotation.eulerAngles.z > 135 && CanSwitchAnimation) {
			SwitchAnimState (AnimStates.Right);
			Img.transform.localScale = new Vector3 (Mathf.Abs(Img.transform.localScale.x), Img.transform.localScale.y, Img.transform.localScale.z);
		} else if (AnimState != AnimStates.Front && transform.rotation.eulerAngles.z <= 135 && transform.rotation.eulerAngles.z > 45 && CanSwitchAnimation) {
			SwitchAnimState (AnimStates.Front);
			Img.transform.localScale = new Vector3 (Mathf.Abs(Img.transform.localScale.x), Img.transform.localScale.y, Img.transform.localScale.z);
		}

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

	void AllowSwitchAnimation ()
	{
		CanSwitchAnimation = true;
	}

	public void CancelWalk ()
	{
		if (canvas != null)
			canvas.Clear ();
	}

	public void GiveProduct (Item.Types Type)
	{

	}

	public static float Angle (Vector3 p1, Vector3 p2)
	{
		// Direction vector. Line to follow to fly to the given object
		Vector3 direction = p1 - p2;
		
		// Direction vector to angle in degrees
		return 90 - (Mathf.Atan2 (direction.x, direction.y) * Mathf.Rad2Deg);
	}

}
