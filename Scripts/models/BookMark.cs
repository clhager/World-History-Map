using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMark {

	public int year;
	public Country[] countries;
	public Dictionary<string, int> countryRenderDates;

	public Country[] getCountries() {
		return countries;
	}

	public bool hasCountry(string countryTag) {
		return countryRenderDates.ContainsKey(countryTag);
	}

	public int getCountryRenderDate(string countryTag) {
		return countryRenderDates [countryTag];
	}

}
