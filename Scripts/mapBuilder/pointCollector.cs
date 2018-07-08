using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointCollector : MonoBehaviour {

	public GameObject points;

	// Use this for initialization
	void Start () {
		collectPoints (this.points);
	}

	public static Dictionary<string, Vector3> collectPoints(GameObject points) {
		Dictionary<string, Vector3> pointDict = new Dictionary<string, Vector3>();
		foreach (Transform child in points.transform) {
			// Check if the child is a point
			pointLabel label = child.GetComponent<pointLabel> ();
			if (label) {
				pointDict.Add (child.name, child.transform.position);
			} else {
				Dictionary<string, Vector3> childDict = collectPoints (child.gameObject);
				foreach (KeyValuePair<string, Vector3> item in childDict) {
					pointDict.Add (item.Key, item.Value);
				}
			}
		}
		return pointDict;
	}
}
