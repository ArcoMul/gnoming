using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public Button ButtonSkip;

	public delegate void OnDoneEventHandler ();
	public event OnDoneEventHandler Done;
	public virtual void OnDone ()
	{
		if (Done != null) {
			Done();
		}
	}

	void Start () {
		ButtonSkip.Click += OnSkip;
	}

	void OnSkip () {
		gameObject.SetActive (false);
		OnDone ();
	}
}
