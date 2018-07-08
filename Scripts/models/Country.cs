using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country {

	public GameObject mapHolder;
	public string countryTag;
	public int startYear;
	public int? endYear;
	public Material material;
	public Dictionary<int, Vector3[][]> points;

	public void create() {
		mapHolder = new GameObject (countryTag);
	}

	public void clearChildMeshes(GameObject parentObject) {
		foreach (GameObject mesh in parentObject.transform) {
			GameObject.Destroy (mesh);
		}
	}

	public void destroy() {
		clearChildMeshes (mapHolder);
		GameObject.Destroy (mapHolder);
	}

	public void render(int year) {

		// Move the previous meshes to a new holder
		GameObject oldHolder = mapHolder;
		create ();
		
		// Retrieve the map starting at that year
		Vector3[][] map = points [year];

		// Render each Vector3[]
		foreach (Vector3[] pointList in map) {
			GameObject countryMesh = meshRender.renderCountryMesh (countryTag, material, pointList);
			// Second mesh for second map
			GameObject countryMesh2 = meshRender.renderCountryMesh (countryTag, material, pointList);
			countryMesh.transform.parent = mapHolder.transform;
			countryMesh2.transform.parent = mapHolder.transform;
			countryMesh2.transform.position = new Vector3 (countryMesh2.transform.position.x + 63.1f, 
														   countryMesh2.transform.position.y, 
														   countryMesh2.transform.position.z);
		}

		// Remove previous meshes
		if (oldHolder) {
			clearChildMeshes (oldHolder);
			GameObject.Destroy (oldHolder);
		}

	}


}
