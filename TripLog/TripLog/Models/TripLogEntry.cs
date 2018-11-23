namespace TripLog.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class TripLogEntry
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string Notes { get; set; }

        public TripLogEntry(string id, int rating) : this()
        {
            Id = id;
            Rating = rating;
        }

        public TripLogEntry()
        {
            Title = string.Empty;
            Id = Guid.NewGuid().ToString();
            Notes = string.Empty;
        }

        public static string Serialize(TripLogEntry entry)
        {
            return JsonConvert.SerializeObject(entry);
        }

        public static TripLogEntry Deserialize(string serializedTripLogEntry)
        {
            var entry = JsonConvert.DeserializeObject<TripLogEntry>(serializedTripLogEntry);

            TripLogEntry result = new TripLogEntry();

            result.Id = entry.Id;
            result.Title = entry.Title;
            result.Latitude = entry.Latitude;
            result.Longitude = entry.Longitude;
            result.Date = entry.Date;
            result.Rating = entry.Rating;
            result.Notes = entry.Notes;

            return result;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TripLogEntry))
            {
                return false;
            }

            var instance = (TripLogEntry) obj;

            return instance.Id.Equals(Id) &&
                   instance.Date.Equals(Date) &&
                   instance.Latitude.Equals(Latitude) &&
                   instance.Longitude.Equals(Longitude) &&
                   instance.Notes.Equals(Notes) &&
                   instance.Rating.Equals(Rating) &&
                   instance.Title.Equals(Title);
        }

        public override int GetHashCode()
        {
            var hashCode = -1009551849;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + Latitude.GetHashCode();
            hashCode = hashCode * -1521134295 + Longitude.GetHashCode();
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + Rating.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Notes);
            return hashCode;
        }
    }
}

