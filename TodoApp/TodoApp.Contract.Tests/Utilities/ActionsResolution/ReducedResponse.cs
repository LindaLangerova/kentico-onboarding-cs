using System;
using System.Net;
using System.Net.Http;

namespace TodoApp.Contract.Tests.Utilities.ActionsResolution
{
    public class ReducedResponse
    {
        public ReducedResponse(HttpResponseMessage message)
        {
            Location = message.Headers.Location;
            StatusCode = message.StatusCode;
        }

        public Uri Location { get; }
        public HttpStatusCode StatusCode { get; }
    }

    public class ReducedResponse<TContent> : ReducedResponse
    {
        public ReducedResponse(HttpResponseMessage message) : base(message)
        {
            message.TryGetContentValue(out TContent content);
            Content = content;
        }

        public TContent Content { get; }
    }
}
