using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshRender : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//renderCountryMesh (points, countryColor, "Ireland");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static GameObject renderCountryMesh(string countryName, Material countryMaterial, Vector3[] countryPoints) {
		GameObject country = new GameObject (countryName);
		Mesh mesh = new Mesh ();

		country.AddComponent<MeshRenderer> ();
		country.GetComponent<MeshRenderer> ().material = countryMaterial;
		country.AddComponent<MeshFilter> ();
		country.GetComponent<MeshFilter> ().mesh = mesh;

		mesh.vertices = countryPoints;

		Triangulator tr = new Triangulator (countryPoints);
		int[] triangles = tr.Triangulate ();
		mesh.triangles = triangles;

		mesh.RecalculateNormals ();

		return country;
	}
}
