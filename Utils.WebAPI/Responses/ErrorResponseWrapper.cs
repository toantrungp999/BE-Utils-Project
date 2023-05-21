using Newtonsoft.Json;

namespace Utils.WebAPI.Responses
{
    public class ErrorResponseWrapper
    {
        [JsonProperty("isError")]
        public bool IsError { get; set; }

        [JsonProperty("errors")]
        public IList<ErrorResponse> Errors { get; set; }

        public void AddError(ErrorResponse error)
        { 
            Errors.Add(error); 
        }

        public static ErrorResponseWrapper CreateInstance()
        {
            return new ErrorResponseWrapper
            {
                IsError = true,
                Errors = new List<ErrorResponse>(),
            };
        }
    }
}
