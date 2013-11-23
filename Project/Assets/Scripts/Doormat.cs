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
		Gnome gnome = collider.gameObject.transform.parent.gameObject.GetComponent<Gnome>();
		OnGnomeEnter (gnome);
	}
}
