using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapManager : MonoBehaviour {

	public int year;                               // Current year of the mapManager
	public Dictionary<string, Country> countries;  // Map of countryTag to country

	public void goToBookMark(BookMark bookMark) {

		// Update or create every country in the bookMark
		foreach (Country country in bookMark.getCountries()) {
			
			country.render (bookMark.getCountryRenderDate (country.countryTag));
			if (!countries.ContainsKey(country.countryTag)) {
				countries.Add (country.countryTag, country);
			}
		}

		// Remove countries that are not in the bookMark
		foreach (KeyValuePair<string, Country> country in countries) {
			if (!bookMark.hasCountry(country.Value.countryTag)) {
				countries.Remove (country.Value.countryTag);
			}
		}

	}
}
