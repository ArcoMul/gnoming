using UnityEngine;
using System.Collections;

public class BodyCollider : MonoBehaviour {

	public Gnome gnome;

	void OnTriggerEnter2D (Collider2D collider)
	{
		Debug.Log ("Trigger");
		gnome.CancelWalk();
	}
}
