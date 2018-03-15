using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace TodoApp.Api.Tests.Utilities.ActionsResolution
{
    internal static class ActionsResolutionExtensions
    {
        public static async Task<HttpResponseMessage> ResolveAction<TController>(this TController controller, Func<TController, Task<IHttpActionResult>> actionSelector)
            => await controller.GetResponse(actionSelector);

        public static async Task<ReducedResponse<TContent>> BeItReducedResponse<TContent>(this Task<HttpResponseMessage> message)
            => new ReducedResponse<TContent>(await message);

        public static async Task<ReducedResponse> BeItReducedResponse(this Task<HttpResponseMessage> message)
            => new ReducedResponse(await message);

        private static async Task<HttpResponseMessage> GetResponse<TController>(this TController controller, Func<TController, Task<IHttpActionResult>> actionSelector)
        {
            var actionResult = await actionSelector(controller);
            return await actionResult.ExecuteAsync(CancellationToken.None);
        }
    }
}
