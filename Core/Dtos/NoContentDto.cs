using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Dtos {
    public class NoContentDto {
        public IList<String>? Errors { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }


        public static NoContentDto Success(int statusCode)
        {
            return new NoContentDto
            {
                StatusCode = statusCode
            };
        }

        public static NoContentDto Fail(List<String> errors, int statusCode)
        {
            return new NoContentDto
            {
                Errors = errors,
                StatusCode = statusCode
            };
        }

        public static NoContentDto Fail(string error, int statusCode)
        {
            return new NoContentDto
            {
                Errors = new List<string> { error },
                StatusCode = statusCode
            };
        }
    }
}
