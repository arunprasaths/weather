using Backend.Models;
using Backend.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Backend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WeatherController : ApiController
    {

       
        [Route("api/weather/{city}")]
        public async Task<IHttpActionResult> GetWeather(string city, string units="metric")
        {

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(Constants.WEATHER_BASE_URL);
                    var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid={Constants.APP_KEY}&units={units}");
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var weatherData = JsonConvert.DeserializeObject<WeatherResponse>(stringResult);
                    return Ok<WeatherResponse>(weatherData);
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest("Getting an error while opening weather api : " + httpRequestException.Message);
                }
            }
        }


        private string DefineExclude(string forecastType)
        {
            switch (forecastType)
            {
                case "1hour": //1hour //minutely
                    return "current,hourly,daily,alerts";
                case "2day"://2day //hourly
                    return "current,minutely,daily,alerts";
                case "7day": //7day //daily
                    return "current,minutely,hourly,alerts";
                default:
                    return "";
            }
        }

        [Route("api/weather/{city}/forecast/{forecastType}")]
        public async Task<IHttpActionResult> GetForecast(string city, string forecastType, string units = "metric")
        {
            var exclude = DefineExclude(forecastType);

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(Constants.WEATHER_BASE_URL);
                    var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid={Constants.APP_KEY}&units={units}");
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var cityWeather = JsonConvert.DeserializeObject<WeatherResponse>(stringResult);

                    var forecastResponse = await client.GetAsync($"/data/2.5/onecall?lat={cityWeather.Coord.Lat}&lon={cityWeather.Coord.Lon}&exclude={exclude}&appid={Constants.APP_KEY}&units={units}");
                    var stringResultForForecast = await forecastResponse.Content.ReadAsStringAsync();
                    var weatherForecastData = JsonConvert.DeserializeObject<WeatherForecastResponse>(stringResultForForecast);
                    return Ok(weatherForecastData);
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest("Getting an error while opening forecast api : " + httpRequestException.Message);
                }
            }
        }        
    }
}
