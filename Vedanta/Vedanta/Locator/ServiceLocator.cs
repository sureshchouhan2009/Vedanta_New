using System;
using System.Collections.Generic;
using System.Text;
using Vedanta.Service;
using Vedanta.Service.Interface;
using Xamarin.Forms;

namespace Vedanta.Locator
{
    public class ServiceLocator
    {
        public static void Init()
        {
            DependencyService.Register<IApiService, ApiService>();
        }
    }
}
