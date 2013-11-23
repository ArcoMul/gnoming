using UnityEngine;
using System.Collections;
using System.Threading;

public class House : MonoBehaviour
{
	public enum Angry { Good, Normal, Bad, Worse};

	private const int TimeToBeAngry = 1000;
	private int angryCounter = 0;
	private Angry angryState;

	public Doormat doormat;
	public enum Statuses {Idle, Requesting, Offering}
	public Statuses Status;
	public Item CurrentItem;

	private Timer angryTime;

	GameObject Icon;

	void Start ()
	{
		doormat.GnomeEnter += OnGnomeEnter;
		angryTime =  new Timer( new TimerCallback( checkAngry), this, 5000, 100);

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
	}

	public void Offer (Item.Types itemType)
	{
		Status = Statuses.Offering;
		CurrentItem = new Item () { Type = itemType };		
		DrawRequirement ();
	}

	private void TalkWithGnome( Gnome gnome ) 
	{
		if (Status == Statuses.Requesting) {
			if( gnome.hasItem ) 
			{
				if( gnome.CurrentItem.Type == CurrentItem.Type ) // gnome has our requesting item
				{
					// stop angryTime
					Status = Statuses.Idle;
					DrawIdle();
					// sum points??
				}
			}
		}

		if (Status == Statuses.Offering)
		{
			if( gnome.CarryItem( CurrentItem ))
			{
				Status = Statuses.Idle;
				DrawIdle();
			}
		}
	}

	private void checkAngry(object state)
	{
		if (angryCounter > 0 && angryCounter <  250) angryState = Angry.Good;
		if (angryCounter > 250 && angryCounter <  500) angryState = Angry.Normal;
		if (angryCounter > 750 && angryCounter <  TimeToBeAngry) angryState = Angry.Bad;
		if (angryCounter == TimeToBeAngry) angryState = Angry.Worse;
		DrawAngryState (angryState);
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
		// draw icon
		Icon = (GameObject) Instantiate (Resources.Load ("Icon"));
		Icon.transform.parent = transform;
		Icon.transform.localPosition = new Vector3 (0, 0.7f, -0.1f);
		
		GameObject o = (GameObject) Instantiate (Resources.Load ("Items/" + CurrentItem.Type.ToString()));
		o.transform.parent = Icon.transform;
		o.transform.localPosition = new Vector3 (0,0,-0.1f);
	}

	private void DrawAngryState() 
	{

	}

	void OnGnomeEnter (Gnome gnome)
	{
		TalkWithGnome (gnome);
	}





}
