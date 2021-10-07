using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunTranslationsAPI.Views.Responses
{
    public class Success
    {
        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class Contents
    {
        [JsonProperty("translation")]
        public string Translation { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("translated")]
        public string Translated { get; set; }
    }

    /// <summary>
    /// The generic response body of a translated text
    /// </summary>
    public class TranslationRespDto
    {
        [JsonProperty("success")]
        public Success Success { get; set; }

        [JsonProperty("contents")]
        public Contents Contents { get; set; }
    }
}
