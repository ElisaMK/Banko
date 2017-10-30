using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Banko1 {
    /// <summary>
    /// Interaction logic for JackpotPuljePage.xaml
    /// </summary>
    public partial class JackpotPuljePage : Page {
        internal ObservableCollection<int> puljeList = new ObservableCollection<int>();
        internal ObservableCollection<int> jackpotList = new ObservableCollection<int>();

        public JackpotPuljePage() {
            InitializeComponent();

            puljeListBox.ItemsSource = puljeList;
            jackpotListBox.ItemsSource = jackpotList;
        }
    }
}
