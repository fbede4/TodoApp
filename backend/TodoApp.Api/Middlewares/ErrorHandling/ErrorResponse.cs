using System.Collections.Generic;

namespace TodoApp.Api.Middlewares.ErrorHandling
{
    /// <summary>
    /// General error response for REST API calls
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Main message of the error.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Identifier of the request that generated this error
        /// </summary>
        public string TraceIdentifier { get; set; }

        /// <summary>
        /// Stacktrace, this should only have a value in dev mode
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// Detailed errors for fields, entities, events, etc.
        /// </summary>
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
