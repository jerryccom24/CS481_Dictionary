using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace hw6
{   //OUR CLASS MADE FROM https://app.quicktype.io/#l=cs&r=json2csharp

    public partial class DictionaryItem
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("definition")]
        public string Definition { get; set; }

        [JsonProperty("example")]
        public string Example { get; set; }
    }

    public partial class DictionaryItem
    {
        public static DictionaryItem[] FromJson(string json) => JsonConvert.DeserializeObject<DictionaryItem[]>(json, hw6.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this DictionaryItem[] self) => JsonConvert.SerializeObject(self, hw6.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}