using Itb.Core.GUI;
using Microsoft.Practices.Prism.MefExtensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FreundeVerwaltung
{
    public partial class App : ApplicationBase
    {
        [System.STAThreadAttribute()]
        public static void Main()
        {
            var app = new App();
            app.Run();
        }

        protected override MefBootstrapper CreateBootstrapper()
        {
            return new Bootstrapper();
        }

    }

}
