using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Canvas : MonoBehaviour
{
	public static int CanvasCount = 0;

	bool Enabled = true;

	bool Drawing = true;

	bool MouseOver = true;

	public List<Vector3> Points = new List<Vector3>();

	Color c1 = new Color (0f, 0f, 0f, 0.1f);
	Color c2 = Color.black;

	LineRenderer LineRendererC;

	void Start ()
	{
		LineRendererC = gameObject.AddComponent<LineRenderer>();
		LineRendererC.material = Resources.Load<Material>("Line");
		LineRendererC.SetColors(c1, c1);
		LineRendererC.SetWidth(0.05f, 0.05f);
		LineRendererC.SetVertexCount(Points.Count);
	}

	void OnMouseOver ()
	{
		if (Enabled && Drawing && Application.platform != RuntimePlatform.Android) {
			MouseOver = true;
		}
	}

	void OnMouseLeave ()
	{

	}

	void Update ()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (Enabled && Drawing && Input.touches.Length > 0)
			{
				Vector3 TouchPos = Input.touches[0].position;
				TouchPos.z = 8;
				Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3(TouchPos.x, TouchPos.y, 0));
				pos = new Vector3 (pos.x, pos.y, -3);
				Points.Add (pos);
				
				LineRendererC.SetVertexCount(Points.Count);
			}
			else if (Input.touches.Length == 0)
			{
				Disable ();
			}
		}

//		Debug.Log ("Enable: " + Enabled);
//		Debug.Log ("Drawing: " + Drawing);
//		Debug.Log ("MouseOver: " + MouseOver);
		if (Enabled && Drawing && MouseOver && Input.GetMouseButton (0) && Application.platform != RuntimePlatform.Android)
		{
			GameObject.Find ("Debug").GetComponent<TextMesh>().text = "MouseOver";
			
			Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
			pos = new Vector3 (pos.x, pos.y, -3);
			Points.Add (pos);
			
			LineRendererC.SetVertexCount(Points.Count);
		} else if (Application.platform != RuntimePlatform.Android && (!MouseOver || !Input.GetMouseButton (0))) {
			Disable ();
		}

		int j = 0;
		foreach (Vector3 p in Points) {
			LineRendererC.SetPosition(j, p);
			j++;
		}
	}

	public void Enable ()
	{
		Debug.Log ("Enable");
		Enabled = true;
	}

	public void Disable ()
	{
		Enabled = false;
		// Points.RemoveAt (Points.Count - 1);
	}

	public Vector3 GetPoint (int i)
	{
		return Points[i];
	}

	public void RemoveStep (int i)
	{
		Points.RemoveAt (i);
	}

	public void SetStep (int i, Vector3 v)
	{
		Points[i] = v;
	}
}
