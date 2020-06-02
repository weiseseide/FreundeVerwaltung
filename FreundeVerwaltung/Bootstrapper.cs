using Itb.Core.MEF;
using System.ComponentModel.Composition.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreundeVerwaltung
{
    class Bootstrapper : BootstrapperBase
    {
        protected override void ConfigureDirectoryCatalog()
        {
            DirectoryCatalog catalog = new DirectoryCatalog(".", "FreundeVerwaltung.*.dll");
            this.AggregateCatalog.Catalogs.Add(catalog);
        }
    }
}