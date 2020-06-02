using Itb.Core.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FreundeVerwaltung.Views
{
    /// <summary>
    /// Interaction logic for DBEditDialogView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class DBEditDialogView : DialogBase
    {
        public DBEditDialogView()
        {
            InitializeComponent();
        }

        private void DialogBase_Loaded(object sender, RoutedEventArgs e)
        {
            this.Activate();
        }
    }
}
