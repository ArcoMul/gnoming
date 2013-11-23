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
	public bool Request (Item.Types itemType)
	{
		if (Status != Statuses.Idle) {
			return false;
		}

		Status = Statuses.Requesting;
		CurrentItem = new Item () { Type = itemType };
		DrawRequirement ();
		InvokeRepeating ("CheckAngry", 60, 60);

		return true;
	}

	public bool Offer (Item.Types itemType)
	{
		if (Status != Statuses.Idle) {
			return false;
		}

		Status = Statuses.Offering;
		CurrentItem = new Item () { Type = itemType };		
		DrawOffer ();
		InvokeRepeating ("CheckAngry", 60, 60);

		return true;
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
		Transform r = transform.FindChild ("Request");
		r.gameObject.SetActive (true);
		r.FindChild ("Item").GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Items/Sprites/" + CurrentItem.Type.ToString());
	}

	private void DrawOffer() 
	{
		Transform r = transform.FindChild ("Offer");
		r.gameObject.SetActive (true);
		r.FindChild ("Item").GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Items/Sprites/" + CurrentItem.Type.ToString());
	}

	private void DrawIdle() 
	{
		Transform r = transform.FindChild ("Offer");
		r.gameObject.SetActive (false);

		r = transform.FindChild ("Request");
		r.gameObject.SetActive (false);
	}

	private void DrawAngryState(Angry AngryState) 
	{

	}

	void OnGnomeEnter (Gnome gnome)
	{
		Debug.Log ("OnGnomeEnter");
		TalkWithGnome (gnome);
	}





}
