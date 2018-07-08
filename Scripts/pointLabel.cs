using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class pointLabel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmos()
	{
		GUIStyle textStyle = new GUIStyle ();
		textStyle.fontSize = 11;
		Handles.Label(this.transform.position, this.name, textStyle);
	}
}

