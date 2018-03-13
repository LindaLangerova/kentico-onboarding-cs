using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace TodoApp.Contract
{
    public interface IUnityBootstrapper
    {
        void RegisterTypes(IUnityContainer container);
    }
}
