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
        public string Type { get; set; }
        public string SecondType { get; set; }
        public string Equipment { get; set; }
        public DateTime? StartTime { get; set; }
        public double? TotalDistance { get; set; }
        public double? Duration { get; set; }
        public int? AverageHeartrate { get; set; }
        public List<HeartRate> HeartRate { get; set; }
        public double? TotalCalories { get; set; }
        public string Notes { get; set; }
        public List<Path> Path { get; set; }
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
            if (StartTime.HasValue)
            {
                json.Append(',');
                PutKeyValuePair("start_time", StartTime.Value.ToString("r", new DateTimeFormatInfo()), ref json);
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
                PutArray<List<HeartRate>, HeartRate>("heart_rate", HeartRate, ref json);
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
                PutArray<List<Path>, Path>("path", Path, ref json);
            }
            
            json.Append('}');
        }

        // JSON-ize Array
        protected void PutArray<T, U>(string key, T val, ref StringBuilder json) 
            where T : ICollection<U> 
            where U : IJSONable
        {
            json.Append('"');
            json.Append(key);
            json.Append('"');
            json.Append(':');

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

        public double Timestamp { get; set; }
        public int Heartrate { get; set; }
    }

    public class Path : IJSONable
    {
        public override void ToJSON(ref StringBuilder json)
        {
            json.Append('{');
            PutKeyValuePair("timestamp", Timestamp, ref json);
            if (Latitude.HasValue)
            {
                json.Append(',');
                PutKeyValuePair("latitude", Latitude.Value, ref json);
            }
            if (Longitude.HasValue)
            {
                json.Append(',');
                PutKeyValuePair("longitude", Longitude.Value, ref json);
            }
            if (Altitude.HasValue)
            {
                json.Append(',');
                PutKeyValuePair("altitude", Altitude.Value, ref json);
            }
            json.Append('}');
        }

        public double Timestamp { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Altitude { get; set; }
        public enum Type { start, end, gps, pause, resume, manual }
    }
}
