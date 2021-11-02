using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class WeatherResponse
    {
        public string Name { get; set; }
        public Main Main { get; set; }
        public IEnumerable<Weather> Weather { get; set; }
        public Coord Coord { get; set; }
    }

    public class Main
    {
        public string Temp { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
    }

    public class Weather
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }

    public class Coord
    {
        public string Lon { get; set; }
        public string Lat { get; set; }
    }
}