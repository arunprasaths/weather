using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Controllers
{
    public class WeatherForecastResponse
    {
        public IEnumerable<Minutely> Minutely { get; set; }
        public IEnumerable<Hourly> Hourly { get; set; }
        public IEnumerable<Daily> Daily { get; set; }
    }

    public class Daily
    {
        public string Dt { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
        public IEnumerable<Weather> Weather { get; set; }
    }

    public class Minutely
    {
        public string Dt { get; set; }
        public string Precipitation { get; set; }
    }

    public class Hourly
    {
        public string Dt { get; set; }
        public string Temp { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
        public IEnumerable<Weather> Weather { get; set; }
    }
}