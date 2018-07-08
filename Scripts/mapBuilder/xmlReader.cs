using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class xmlReader : MonoBehaviour {

	public GameObject points;
	public Dictionary<string, Vector3> pointDict;


	// Use this for initialization
	void Start () {
		pointDict = pointCollector.collectPoints (this.points);

		XmlDocument country = openFileAsXML ("Assets/Data/Countries/IRE.xml");
		Country ireland = readCountryXML (country, pointDict);
		ireland.create ();
		ireland.render (1922);
		XmlDocument country2 = openFileAsXML ("Assets/Data/Countries/ENG.xml");
		Country england = readCountryXML (country2, pointDict);
		england.create ();
		england.render (1922);
		XmlDocument country3 = openFileAsXML ("Assets/Data/Countries/Background.xml");
		Country background = readCountryXML (country3, pointDict);
		background.create ();
		background.render (1922);
		points.SetActive (false);
	}

	public static XmlDocument openFileAsXML(string path) {
		if (!System.IO.File.Exists (path)) {
			print ("ERROR: File <" + path + "> not found.");
			return null;
		}
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(System.IO.File.ReadAllText(path));
		return xmlDoc;
	}

	public static string[] readCountryList (XmlDocument countryIndex) {
		List<string> countryTags = new List<string> ();
		XmlNode index = countryIndex.GetElementsByTagName ("index") [0];
		foreach (XmlNode tag in index.ChildNodes) {
			if (tag.Name == "tag") {
				countryTags.Add (tag.InnerText);
			}
		}
		return countryTags.ToArray ();
	}

	public static Country readCountryXML(XmlDocument countryDoc, Dictionary<string, Vector3> pointDict) {
		
		// Set up the new country's object
		Country newCountry = new Country ();
		newCountry.material = new Material (Shader.Find("Standard"));
		newCountry.points = new Dictionary<int, Vector3[][]> ();

		// Process the XML
		XmlNode country = countryDoc.GetElementsByTagName ("country") [0];
		foreach (XmlNode child in country.ChildNodes) {

			switch (child.Name) {

			case "header":
				Dictionary<string, string> header = readHeader (child);
				newCountry.countryTag = header ["tag"];
				newCountry.startYear = int.Parse (header ["start"]);
				if (header ["end"] == "NULL") {
					newCountry.endYear = null;
				} else {
					newCountry.endYear = int.Parse (header ["end"]);
				}
				Color countryColor;
				ColorUtility.TryParseHtmlString (header ["color"], out countryColor);
				newCountry.material.color = countryColor;
				break;

			// Reads the different maps and adds them to the dictionary of maps
			case "mapLayouts":
				foreach (XmlNode map in child.ChildNodes) {
					readCountryMap (map, newCountry.points, pointDict);
				}
				break;
			}

		}
		return newCountry;

	}

	public static Country readBackgroundXML(XmlDocument backgroundDoc, Dictionary<string, Vector3> pointDict) {
		
		// Set up the new country's object
		Country background = new Country ();
		background.material = new Material (Shader.Find("Standard"));
		background.points = new Dictionary<int, Vector3[][]> ();

		// Process the XML
		XmlNode backgroundNode = backgroundDoc.GetElementsByTagName ("background") [0];
		List<Vector3[]> backgroundPointLists = new List<Vector3[]> ();
		foreach (XmlNode pointList in backgroundNode.ChildNodes) {
			backgroundPointLists.Add (readPointList (pointList, pointDict));
		}
		background.points.Add(0, backgroundPointLists.ToArray ());
		return background;

	}

	/**
	 * header
	 */
	public static Dictionary<string, string> readHeader(XmlNode header) {
		
		Dictionary<string, string> headerDict = new Dictionary<string, string> ();

		foreach (XmlNode child in header.ChildNodes) {
			headerDict.Add (child.Name, child.InnerText);
		}

		return headerDict;
	}

	/**
	* leaders
	*/

	/**
	* govTypes
	*/

	/**
	 * mapLayouts
	 */

	public static void readCountryMap(XmlNode map, Dictionary<int, Vector3[][]> countryPoints, Dictionary<string, Vector3> pointDict) {
		int mapYear = -1;
		List<Vector3[]> countryPointLists = new List<Vector3[]> ();
		foreach (XmlNode child in map.ChildNodes) {

			switch (child.Name) {

			case "start":
				mapYear = int.Parse (child.InnerText);
				break;

			case "pointList":
				countryPointLists.Add (readPointList (child, pointDict));
				break;

			}

		}
		if (mapYear == -1) {
			print ("No start year in map!");
		}
		countryPoints.Add(mapYear, countryPointLists.ToArray());
	}
		
	public static Vector3[] readPointList(XmlNode pointList, Dictionary<string, Vector3> pointDict) {
		
		List<Vector3> countryPointPath = new List<Vector3> ();

		// Convert the paths into a list of points
		foreach (XmlNode child in pointList.ChildNodes) {
			string[] pointPath = child.InnerText.Split('/');
			if (pointPath.Length != 3) {
				print ("Error: point must be in the format: <path/start/end>: " + child.InnerText);
			}
			if (int.Parse (pointPath [1]) < int.Parse (pointPath [2])) {
				for (int i = int.Parse (pointPath [1]); i <= int.Parse (pointPath [2]); i++) {
					try {
						countryPointPath.Add (pointDict [pointPath [0] + i]);
					} catch {
						print (pointPath [0] + i);
					}
				}
			} else {
				for (int i = int.Parse (pointPath [1]); i >= int.Parse (pointPath [2]); i--) {
					try {
						countryPointPath.Add (pointDict [pointPath [0] + i]);
					} catch {
						print (pointPath [0] + i);
					}
				}
			}
		}
		return countryPointPath.ToArray();
	}
}
