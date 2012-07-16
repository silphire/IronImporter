using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace IronImporter
{
    public abstract class IJSONable // RFC 4627
    {
        public abstract void ToJSON(ref StringBuilder json);

        public string ToJSON()
        {
            StringBuilder json = new StringBuilder();
            ToJSON(ref json);
            return json.ToString();
        }

        public void PutKeyValuePair<T>(string key, T val, ref StringBuilder json)
        {
            json.Append('"');
            json.Append(key);
            json.Append('"');

            json.Append(':');

            json.Append(val.ToString());
        }

        public void PutKeyValuePair(string key, string val, ref StringBuilder json)
        {
            json.Append('"');
            json.Append(key);
            json.Append('"');

            json.Append(':');

            json.Append('"');
            json.Append(val.ToString());
            json.Append('"');
        }
    }

    public class HealthGraph : IJSONable
    {
        private string Type { get; set; }
        private string SecondType { get; set; }
        private string Equipment { get; set; }
        private DateTime StartTime { get; set; }
        private double? TotalDistance { get; set; }
        private double? Duration { get; set; }
        private int? AverageHeartrate { get; set; }
        private List<HeartRate> HeartRate { get; set; }
        private double? TotalCalories { get; set; }
        private string Notes { get; set; }
        private List<Path> Path { get; set; }
        // post_to_twitter
        // post_to_facebook
        // detect_pause

        public override void ToJSON(ref StringBuilder json)
        {
            json.Append('{');

            PutKeyValuePair("type", Type, ref json);
            if (SecondType != null)
            {
                json.Append(',');
                PutKeyValuePair("second_type", SecondType, ref json);
            }
            if (Equipment != null)
            {
                json.Append(',');
                PutKeyValuePair("equipment", SecondType, ref json);
            }
            if (StartTime != null)
            {
                json.Append(',');
                PutKeyValuePair("start_time", StartTime.ToString("r", new DateTimeFormatInfo()), ref json);
            }
            if (TotalDistance != null)
            {
                json.Append(',');
                PutKeyValuePair("total_distance", TotalDistance, ref json);
            }
            if (Duration != null)
            {
                json.Append(',');
                PutKeyValuePair("duration", Duration, ref json);
            }
            if (AverageHeartrate != null)
            {
                json.Append(',');
                PutKeyValuePair("average_heart_rate", AverageHeartrate, ref json);
            }
            if (HeartRate != null && HeartRate.Count > 0)
            {
                json.Append(',');
                PutKeyValuePair("heart_rate", HeartRate, ref json);
            }
            if (TotalCalories != null)
            {
                json.Append(',');
                PutKeyValuePair("total_calories", TotalCalories, ref json);
            }
            if (Notes != null)
            {
                json.Append(',');
                PutKeyValuePair("notes", Notes, ref json);
            }
            if (Path != null && Path.Count > 0)
            {
                json.Append(',');
                PutKeyValuePair("path", Path, ref json);
            }
            
            json.Append('}');
        }

        // JSON-ize Array
        protected void PutKeyValuePair<T, U>(string key, T val, ref StringBuilder json) 
            where T : ICollection<U> 
            where U : IJSONable
        {
            bool isMiddle = false;
            json.Append('[');
            foreach(U t in val)
            {
                if (isMiddle)
                {
                    json.Append(',');
                }
                else
                {
                    isMiddle = true;
                }
                json.Append(t.ToJSON());
            }
            json.Append(']');
        }
    }

    public class HeartRate : IJSONable
    {
        public override void ToJSON(ref StringBuilder json)
        {
            json.Append('{');
            PutKeyValuePair("timestamp", Timestamp, ref json);
            json.Append(',');
            PutKeyValuePair("heart_rate", Heartrate, ref json);
            json.Append('}');
        }

        private double Timestamp { get; set; }
        private int Heartrate { get; set; }
    }

    public class Path : IJSONable
    {
        public override void ToJSON(ref StringBuilder json)
        {
            json.Append('{');
            PutKeyValuePair("timestamp", Timestamp, ref json);
            json.Append(',');
            PutKeyValuePair("latitude", Latitude, ref json);
            json.Append(',');
            PutKeyValuePair("longitude", Latitude, ref json);
            json.Append(',');
            PutKeyValuePair("altitude", Latitude, ref json);
            json.Append('}');
        }

        private double Timestamp { get; set; }
        private double Latitude { get; set; }
        private double Longitude { get; set; }
        private double Altitude { get; set; }
        private enum Type { start, end, gps, pause, resume, manual }
    }
}
