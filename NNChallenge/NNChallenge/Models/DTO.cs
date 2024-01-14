using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNChallenge.Models
{
    public class DTO
    {
        public Location location { get; set; }
        public Forecast forecast { get; set; }
    }

    public class Forecast
    {
        public List<Forecastday> forecastday { get; set; }
    }

    public class Forecastday
    {
        public DateTime Date { get; set; }
        public List<Hour> hour { get; set; }
    }
    public class Hour
    {
        public DateTime time { get; set; }
        public float temp_c { get; set; }
        public float temp_f { get; set; }
        public Condition condition { get; set; }
    }

    public class Condition
    {
        public string icon { get; set; }
    }

    public class Location
    {
        public string name { get; set; }
    }

    public class Current
    {
        public float temp_c { get; set; }
        public float temp_f { get; set; }
    }
}
