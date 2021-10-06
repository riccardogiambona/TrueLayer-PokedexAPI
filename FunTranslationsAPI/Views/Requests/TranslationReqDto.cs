using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunTranslationsAPI.Views.Requests
{
    public class TranslationReqDto
    {
        [JsonProperty("text")]
        public string TextToTranslate { get; set; }
    }
}
