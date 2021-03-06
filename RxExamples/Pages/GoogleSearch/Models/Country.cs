﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace RxExamples.Pages
{
    public class Country
    {
        public string Name { get; set; }

        public string Capital { get; set; } 
        public string Flag { get; set; }

    }

    public static class CountryProvider
    {
        private static List<Country> _all;
        public static List<Country> All => _all ?? (_all = Load());

        private static List<Country> Load()
        {
            var whereToFindCountries = Path.Combine(
                Environment.CurrentDirectory,
                "countries.json"
            );

            using (StreamReader r = new StreamReader(whereToFindCountries))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Country>>(json);
            }
        }
    }
}
