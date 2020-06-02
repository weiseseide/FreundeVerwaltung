using FreundeVerwaltung.DatabaseManager;
using FreundeVerwaltung.Objects;
using Itb.Core.GUI;
using Itb.Core.MEF;
using Itb.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FreundeVerwaltung.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DBEditDialogViewModel : DialogViewModelBase<DBEditDialogViewModel, DialogObject>
    {
        [Import]
        private InteractionController InteractionController { get; set; }

        private IDataBaseManager dbm;
        private bool addNewCustomer;

        [ImportingConstructor]
        public DBEditDialogViewModel(DialogObject obj)
        {
            this.dbm = obj.dbm;

            if(obj.id== null)
            {
                this.EditPerson = new People();
                addNewCustomer = true;
            }
            else
            {
                this.EditPerson = dbm.getPerson((int)obj.id);
                addNewCustomer = false;
            }
            

        }

        
        private DelegateCommand<Window> _cancelCommand;
        public DelegateCommand<Window> CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                    _cancelCommand = new DelegateCommand<Window>((Window input) =>
                    {
                        CloseWindow(input);
                    });

                return _cancelCommand;
            }
            private set { _cancelCommand = value; }
        }

        private People _editPerson;
        public People EditPerson
        {
            get { return _editPerson; }
            set
            {
                if (_editPerson != value)
                {
                    _editPerson = value;
                    NotifyPropertyChanged(nameof(EditPerson));
                }
            }
        }

        protected override bool OnOK()
        {
            try
            {
                if (EditPerson.CustomerID != null && EditPerson.LastName != "" && EditPerson.FirstName != "")
                {
                    if(addNewCustomer)
                        dbm.newEntry(EditPerson);
                    else
                        dbm.editEntry(EditPerson);

                    return true;
                }
                else
                {
                    InteractionController.MessageBox.ShowWarning("Leere Felder","Bitte alle Felder ausfüllen");
                }
                //ToDo return to dialog(?)
                

                
            }
            catch (Exception e)
            {
                InteractionController.MessageBox.ShowException( e.Message, e);
            }

            return false;
        }

        private void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

    }
}
