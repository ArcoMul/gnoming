using UnityEngine;
using System.Collections;

public class House : MonoBehaviour
{
	public Doormat doormat;

	public enum Statuses {Idle, Requesting, Offering}
	public Statuses Status;

	GameObject Icon;

	void Start ()
	{
		doormat.GnomeEnter += OnGnomeEnter;
	}
	
	void Update ()
	{
	
	}

	void OnGnomeEnter (Gnome gnome)
	{
		if (Status == Statuses.Offering)
		{
			gnome.GiveProduct (Item.Types.Drill);
			Destroy (Icon);
		}
	}

	public void Request (Item.Types Type)
	{
		Status = Statuses.Requesting;

		Icon = (GameObject) Instantiate (Resources.Load ("Icon"));
		Icon.transform.parent = transform;
		Icon.transform.localPosition = new Vector3 (0, 0.7f, -0.1f);
		
		GameObject o = (GameObject) Instantiate (Resources.Load ("Items/" + Type.ToString()));
		o.transform.parent = Icon.transform;
		o.transform.localPosition = new Vector3 (0,0,-0.1f);
	}

	public void Offer (Item.Types Type)
	{
		Status = Statuses.Offering;

		Icon = (GameObject) Instantiate (Resources.Load ("Icon"));
		Icon.transform.parent = transform;
		Icon.transform.localPosition = new Vector3 (0, 0.7f, -0.1f);
		
		GameObject o = (GameObject) Instantiate (Resources.Load ("Items/" + Type.ToString()));
		o.transform.parent = Icon.transform;
		o.transform.localPosition = new Vector3 (0,0,-0.1f);
	}

}
