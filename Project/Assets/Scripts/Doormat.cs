using UnityEngine;
using System.Collections;

public class Doormat : MonoBehaviour
{
	public delegate void OnGnomeEnterEventHandler (Gnome gnome);
	public event OnGnomeEnterEventHandler GnomeEnter;
	public virtual void OnGnomeEnter (Gnome gnome)
	{
		if (GnomeEnter != null) {
			GnomeEnter(gnome);
		}
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		Debug.Log ("Tirgger Doormat");
		if (collider.gameObject.transform.parent && collider.gameObject.transform.parent.gameObject.name == "Gnome") {
			Debug.Log ("OnGnomeEnter");
			Gnome gnome = collider.gameObject.transform.parent.gameObject.GetComponent<Gnome>();
			OnGnomeEnter (gnome);
		}
	}
}
