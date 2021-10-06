using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Api.Views
{
    public class ErrorDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        //Here other fields could be added to better describe the error

        public ErrorDto(string name)
        {
            Name = name;
        }
    }
}
