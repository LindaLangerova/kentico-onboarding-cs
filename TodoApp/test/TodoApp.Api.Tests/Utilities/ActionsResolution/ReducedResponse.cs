using System;
using System.Net;
using System.Net.Http;

namespace TodoApp.Api.Tests.Utilities.ActionsResolution
{
    internal class ReducedResponse
    {
        public Uri Location { get; }
        public HttpStatusCode StatusCode { get; }

        public ReducedResponse(HttpResponseMessage message)
        {
            Location = message.Headers.Location;
            StatusCode = message.StatusCode;
        }
    }

    internal class ReducedResponse<TContent> : ReducedResponse
    {
        public TContent Content { get; }

        public ReducedResponse(HttpResponseMessage message) : base(message)
        {
            message.TryGetContentValue(out TContent content);
            Content = content;
        }
    }
}