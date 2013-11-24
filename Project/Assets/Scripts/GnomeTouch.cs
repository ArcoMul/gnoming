using UnityEngine;
using System.Collections;

public class GnomeTouch : MonoBehaviour {

	public Gnome gnome;

	void OnMouseDown ()
	{
		gnome.StartLineDraw ();
	}

}
