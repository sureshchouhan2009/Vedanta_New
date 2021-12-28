using Newtonsoft.Json;

namespace Vedanta.Service
{
        public class Response<T> : BaseLogic
        {
            [JsonProperty(propertyName: "responceType")]
            public string ResponceType { get; set; }

            [JsonProperty(propertyName: "isSuccess")]
            public bool IsSuccess { get; set; }

            [JsonProperty(propertyName: "data")]
            public T Data { get; set; }

            [JsonProperty(propertyName: "errorCode")]
            public string ErrorCode { get; set; }

            [JsonProperty(propertyName: "errorMessage")]
            public string Message { get; set; }
        }

}
