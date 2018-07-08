using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapBuilder : MonoBehaviour {

	public mapManager manager;

	// Build Objects
	public GameObject map1;
	public GameObject map2;
	public GameObject points;     
	public Dictionary<string, Vector3> pointDict;

	// Map Objects
	public Country background;

	/** 
	 * This object builds the map from XML and passes it on to the mapManager
	 */
	void Start () {

		// Build the dictionary of points
		this.pointDict = pointCollector.collectPoints (this.points);

		// Read and build background
		background = xmlReader.readBackgroundXML (xmlReader.openFileAsXML ("Assets/Data/Background.xml"), pointDict);
		background.countryTag = "Background";
		background.create ();
		background.render (0);
		background.mapHolder.transform.position = new Vector3 (background.mapHolder.transform.position.x,
															   background.mapHolder.transform.position.y,
															   1);

		// Read all the countries from country-index
		string[] countryTags = xmlReader.readCountryList (xmlReader.openFileAsXML ("Assets/Data/country-index.xml"));

		BookMark bookMark2000 = new BookMark ();
		bookMark2000.year = 2000;
		bookMark2000.countryRenderDates = new Dictionary<string, int> ();
		List<Country> countryList = new List<Country> ();
		Dictionary<string, Country> countries = new Dictionary<string, Country> ();
		foreach (string tag in countryTags) {
			Country country = xmlReader.readCountryXML (xmlReader.openFileAsXML ("Assets/Data/Countries/" + tag + ".xml"), this.pointDict);
			countryList.Add (country);
			countries.Add (country.countryTag, country);
			foreach (KeyValuePair<int, Vector3[][]> item in country.points) {
				try {
					bookMark2000.countryRenderDates.Add (country.countryTag, item.Key);
				} catch {
					print ("Duplicate country tag: " + country.countryTag);
				}
			}
		}
		bookMark2000.countries = countryList.ToArray ();
		manager.countries = countries;
		manager.goToBookMark (bookMark2000);

		// Deactivate build objects
		map1.SetActive (false);
		map2.SetActive (false);
		points.SetActive (false);

	}
}
