using UnityEngine;
using System.Collections;
using System.Threading;

public class House : MonoBehaviour
{
	public enum Angry { Good = 1, Normal = 2, Bad = 3, Worse = 4};

	private const int TimeToBeAngry = 1000;
	private int angryCounter = 0;
	private Angry AngryState;

	public Doormat doormat;
	public enum Statuses {Idle, Requesting, Offering}
	public Statuses Status;
	public Item CurrentItem;

	private Timer angryTime;

	GameObject Icon;

	public delegate void OnScoreEventHandler ();
	public event OnScoreEventHandler Score;
	public virtual void OnScore ()
	{
		if (Score != null) {
			Score();
		}
	}

	void Start ()
	{
		doormat.GnomeEnter += OnGnomeEnter;
		// angryTime =  new Timer( new TimerCallback( checkAngry ), this, 5000, 100);

	}
	
	void Update ()
	{
	
	}

	/**
	 * 
	 *  House actions
	 *  ANTON
	 * 
	 */
	public void Request (Item.Types itemType)
	{
		Status = Statuses.Requesting;
		CurrentItem = new Item () { Type = itemType };
		DrawRequirement ();
		InvokeRepeating ("CheckAngry", 60, 60);
	}

	public void Offer (Item.Types itemType)
	{
		Status = Statuses.Offering;
		CurrentItem = new Item () { Type = itemType };		
		DrawOffer ();
		InvokeRepeating ("CheckAngry", 60, 60);
	}

	private void TalkWithGnome( Gnome gnome ) 
	{
		Debug.Log ("TalkWithGnome");
		if (Status == Statuses.Requesting)
		{
			Debug.Log ("Requesting");
			if( gnome.HasItem () ) 
			{
				Debug.Log ("HasItem");
				if( gnome.CurrentItem.Type == CurrentItem.Type ) // gnome has our requesting item
				{
					Debug.Log ("Get the item from the gnome");
					gnome.GetItem ();
					CancelInvoke ();
					Status = Statuses.Idle;
					DrawIdle();
					OnScore();
				}
			}
		}

		if (Status == Statuses.Offering)
		{
			Debug.Log ("Offer gnome a item");
			if( gnome.CarryItem( CurrentItem ))
			{
				Debug.Log ("Item given");
				Status = Statuses.Idle;
				DrawIdle();
			}
		}
	}

	private void CheckAngry (object state)
	{
		AngryState++;
		DrawAngryState (AngryState);
	}


	/*
	 * 
	 *  Visual Methods
	 *  ARCO!
	 */
	private void DrawRequirement() 
	{
		// draw icon
		Icon = (GameObject) Instantiate (Resources.Load ("Icon"));
		Icon.transform.parent = transform;
		Icon.transform.localPosition = new Vector3 (0, 0.7f, -0.1f);

		GameObject o = (GameObject) Instantiate (Resources.Load ("Items/" + CurrentItem.Type.ToString()));
		o.transform.parent = Icon.transform;
		o.transform.localPosition = new Vector3 (0,0,-0.1f);
	}

	private void DrawOffer() 
	{
		// draw icon
		Icon = (GameObject) Instantiate (Resources.Load ("Icon"));
		Icon.transform.parent = transform;
		Icon.transform.localPosition = new Vector3 (0, 0.7f, -0.1f);
		
		GameObject o = (GameObject) Instantiate (Resources.Load ("Items/" + CurrentItem.Type.ToString()));
		o.transform.parent = Icon.transform;
		o.transform.localPosition = new Vector3 (0,0,-0.1f);
	}

	private void DrawIdle() 
	{
		if (Icon != null) {
			Destroy (Icon);
		}
	}

	private void DrawAngryState(Angry AngryState) 
	{

	}

	void OnGnomeEnter (Gnome gnome)
	{
		TalkWithGnome (gnome);
	}





}
