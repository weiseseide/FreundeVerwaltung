using CBAP.Shared.Tools;
using FreundeVerwaltung.DatabaseManager;
using FreundeVerwaltung.Objects;
using Itb.Core.MEF;
using Itb.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreundeVerwaltung.ViewModels
{
    [Export(typeof(FreundeMainViewModel))]
    public class FreundeMainViewModel : ViewModelBase<FreundeMainViewModel>
    {
        [Import]
        private InteractionController InteractionController { get; set; }
        private DatabaseManager.IDataBaseManager dbm;

        [ImportingConstructor]
        FreundeMainViewModel(DatabaseManager.IDataBaseManager dbm)
        {
            this.dbm = dbm;
            PeopleDataGrid = this.dbm.refreshData();
            
        }

        private ObservableCollection<People> _peopleDataGrid;
        public ObservableCollection<People> PeopleDataGrid
        {
            get { return _peopleDataGrid; }
            set
            {
                if (_peopleDataGrid != value)
                {
                    _peopleDataGrid = value;
                    NotifyPropertyChanged(nameof(PeopleDataGrid));
                }
            }
        }

        private People _selectedPerson;
        public People SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                if (_selectedPerson != value)
                {
                    _selectedPerson = value;
                    NotifyPropertyChanged(nameof(SelectedPerson));
                }
            }
        }

        private DelegateCommand _newEntryCommand;
        public DelegateCommand NewEntryCommand
        {
            get
            {
                if (_newEntryCommand == null)
                    _newEntryCommand = new DelegateCommand(() =>
                    {
                        DialogObject dialogObjects = new DialogObject(dbm);
                        if (InteractionController.ShowDialog<DBEditDialogViewModel>(dialogObjects))
                        {
                            PeopleDataGrid = dbm.refreshData();
                            
                        }

                    });

                return _newEntryCommand;
            }
            private set { _newEntryCommand = value; }
        }


        private DelegateCommand _editEntryCommand;
        public DelegateCommand EditEntryCommand
        {
            get
            {
                if (_editEntryCommand == null)
                    _editEntryCommand = new DelegateCommand(() =>
                    {
                        if(SelectedPerson != null)
                        {
                            DialogObject dialogObjects = new DialogObject(dbm, SelectedPerson.PersonID);

                            if (InteractionController.ShowDialog<DBEditDialogViewModel>(dialogObjects))
                            {
                                PeopleDataGrid = dbm.refreshData();
                                
                            }
                        }
                        

                    });

                return _editEntryCommand;
            }
            private set { _editEntryCommand = value; }
        }


        private DelegateCommand _deleteEntryCommand;
        public DelegateCommand DeleteEntryCommand
        {
            get
            {
                if (_deleteEntryCommand == null)
                    _deleteEntryCommand = new DelegateCommand(() =>
                    {
                        if (SelectedPerson != null)
                        {
                            if (InteractionController.MessageBox.ShowConfirmation("Löschen", "Wollen Sie " + SelectedPerson.FirstName + " " + SelectedPerson.LastName + " löschen?"))
                            {
                                dbm.deleteEntry(SelectedPerson);
                                PeopleDataGrid = dbm.refreshData();
                            }
                        }

                    });

                return _deleteEntryCommand;
            }
            private set { _deleteEntryCommand = value; }
        }


        private DelegateCommand _refreshDataCommand;
        public DelegateCommand RefreshDataCommand
        {
            get
            {
                if (_refreshDataCommand == null)
                    _refreshDataCommand = new DelegateCommand(() =>
                    {
                        PeopleDataGrid = dbm.refreshData();
                    });

                return _refreshDataCommand;
            }
            private set { _refreshDataCommand = value; }
        }


    }
}
