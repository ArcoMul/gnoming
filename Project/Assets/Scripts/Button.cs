using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
	public string SceneName;

	public delegate void OnClickEventHandler ();
	public event OnClickEventHandler Click;
	public virtual void OnClick ()
	{
		if (Click != null) {
			Click();
		} else if (SceneName != "") {
			Application.LoadLevel (SceneName);
		}
	}

	void OnMouseDown ()
	{
		OnClick ();
	}

}
