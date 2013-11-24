using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Canvas : MonoBehaviour
{
	public Material mat;

	public static int CanvasCount = 0;

	bool Enabled = true;

	bool Drawing = true;

	bool MouseOver = true;

	public List<Vector3> Points = new List<Vector3>();

	Color c1 = new Color (1f, 1f, 1f, 0.4f);
	Color c2 = Color.black;

	CustomLineRenderer LineRendererC;

	void Start ()
	{
		LineRendererC = gameObject.AddComponent<CustomLineRenderer>();
		LineRendererC.Width = 0.08f;
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
				AddPoint (pos);
			}
			else if (Input.touches.Length == 0)
			{
				Disable ();
			}
		}

		if (Enabled && Drawing && MouseOver && Input.GetMouseButton (0) && Application.platform != RuntimePlatform.Android)
		{
			GameObject.Find ("Debug").GetComponent<TextMesh>().text = "MouseOver";

			Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
			AddPoint (pos);
		} else if (Application.platform != RuntimePlatform.Android && (!MouseOver || !Input.GetMouseButton (0))) {
			Disable ();
		}

	}

	void AddPoint (Vector3 Pos)
	{
		Pos = new Vector3 (Pos.x, Pos.y, -3);

		if (Points.Count > 0 && Vector3.Distance (Points[Points.Count - 1], Pos) < 0.1f) return;

		Points.Add (Pos);
		LineRendererC.SetVertexCount(Points.Count);
		LineRendererC.SetPosition(Points.Count - 1, Pos);
	}
	public void Enable ()
	{
		Debug.Log ("Enable");
		Enabled = true;
	}

	public void Disable ()
	{
		Enabled = false;
	}

	public Vector3 GetPoint (int i)
	{
		return Points[i];
	}

	public void RemoveStep (int i)
	{
		Points.RemoveAt (i);
		for (int j = 0; j < Points.Count - 1; j++) {
			LineRendererC.SetPosition (j, Points[j]);
		}
		LineRendererC.SetVertexCount(Points.Count);
	}

	public void SetStep (int i, Vector3 v)
	{
		Points[i] = v;
		LineRendererC.SetPosition (i, v);
	}

	public void Clear ()
	{
		Points.Clear();
		LineRendererC.SetVertexCount(0);
	}
	
}
