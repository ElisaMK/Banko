using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page {
        internal List<int> talList = new List<int>();

        public MainPage() {
            InitializeComponent();

            TalListeGenerator();

            TalTilHvid();
        }
        internal void NyTal_Click(object sender, RoutedEventArgs e) {
            NytTal();
        }

        private void banko_Click(object sender, RoutedEventArgs e) {
        }

        private void nytSpil_Click(object sender, RoutedEventArgs e) {
        }

        internal void NytTal() {
            Random rnd = new Random();

            int Value = rnd.Next(1, talList.Count);
            talLabel.Content = talList[Value];


            foreach (UIElement ele in leftWithNumbers.Children) {
                Label midlertidigLabel = null;
                if (ele.GetType() == typeof(Label)) {
                    Label lablesITaltabel = (Label)ele;

                    if (Convert.ToInt32(lablesITaltabel.Content) == talList[Value]) {
                        midlertidigLabel = lablesITaltabel;
                        midlertidigLabel.Foreground = Brushes.Black;
                    }
                }
            }
            talList.RemoveAt(Value);
        }

        internal void TalListeGenerator() {
            for (int i = 0; i < 91; i++) {
                talList.Add(i);
            }
        }

        internal void TalTilHvid() {
            foreach (UIElement ele in leftWithNumbers.Children) {
                if (ele.GetType() == typeof(Label)) {
                    Label lbl = (Label)ele;
                    lbl.Foreground = Brushes.White;
                    lbl.BorderThickness = new Thickness(1);
                    lbl.BorderBrush = Brushes.LightGray;
                }
            }
        }
    }
}
