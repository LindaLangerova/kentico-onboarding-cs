using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Contract.Services;

namespace TodoApp.Services.Providers
{
    public class RouteNameProvider : IRouteNameProvider
    {
        public string GetRouteName()
        {
            return "DEFAULT_API";
        }

    }
}
