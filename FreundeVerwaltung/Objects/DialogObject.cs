using FreundeVerwaltung.DatabaseManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreundeVerwaltung.Objects
{
    public class DialogObject : INotifyPropertyChanged
    {
        public IDataBaseManager dbm;
        public Nullable<int> id;

        public event PropertyChangedEventHandler PropertyChanged;

        public DialogObject(IDataBaseManager dbm)
        {
            this.dbm = dbm;
            this.id = null;
        }

        public DialogObject(IDataBaseManager dbm, int id)
        {
            this.dbm = dbm;
            this.id = id;
        }
    }
}
