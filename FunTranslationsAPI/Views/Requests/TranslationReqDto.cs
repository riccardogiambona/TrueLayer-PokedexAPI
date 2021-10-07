using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunTranslationsAPI.Views.Requests
{
    /// <summary>
    /// The generic request body to translate a text
    /// </summary>
    public class TranslationReqDto
    {
        [JsonProperty("text")]
        public string TextToTranslate { get; set; }
    }
}
