using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using System.Windows.Threading;


namespace Banko1 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        internal List<int> talList = new List<int>();
        public ObservableCollection<int> puljeList = new ObservableCollection<int>();
        internal ObservableCollection<int> jackpotList = new ObservableCollection<int>();
        internal List<int> brugteTalList = new List<int>();
        internal int antalSpil = 0;

        private string path = @"C:\Banko";
        private String dato = DateTime.Now.ToString("dd.MM.yyy");
        private String tid = DateTime.Now.ToString("HH:mm");

        private Random rnd;

        public MainWindow() {
            InitializeComponent();

            TalListeGenerator();

            TalTilHvid();

            puljeListBox.ItemsSource = puljeList;
            jackpotListBox.ItemsSource = jackpotList;
           
        }


        //klik event metoder
        private void Række_Click(object sender, RoutedEventArgs e) { // der kan være en fejl at der blev trykket på banko
            try {
                if (!puljeList.Contains(Convert.ToInt32(talLabel.Content))) {

                    tekstBoxPopUp.Text = "Banko på række";
                    ShowPopUp();
                    puljeList.Add(Convert.ToInt32(talLabel.Content));
                }

            } catch {
                talLabel.Content = "\"";
            }

        }

        private void fuldPlade_Click(object sender, RoutedEventArgs e) { // der kan være en fejl at der blev trykket på banko
            try {
                if (!puljeList.Contains(Convert.ToInt32(talLabel.Content))) {
                    tekstBoxPopUp.Text = "Banko på fuldplade";
                    PopUp.IsOpen = true;
                    puljeList.Add(Convert.ToInt32(talLabel.Content));
                }
                if (!jackpotList.Contains(Convert.ToInt32(talLabel.Content))) {
                    tekstBoxPopUp.Text = "Banko på fuldplade";
                    ShowPopUp();
                    jackpotList.Add(Convert.ToInt32(talLabel.Content));
                }

            } catch {
                talLabel.Content = "\"";
            }
        }

        private void NytSpil_Click(object sender, RoutedEventArgs e) {
            tekstBoxPopUp.Text = "Spil Gemt";
            ShowPopUp();
            NytSpil();
        }

        private void OpenSpilMappe_Click(object sender, RoutedEventArgs e) {

            OpenFileDialog opd = new OpenFileDialog();
            opd.Multiselect = true;
            opd.Filter = "Text filer (*.txt)|*.txt|Alle typer (*.*)|*.*";
            opd.InitialDirectory = path;
            Nullable<bool> result = opd.ShowDialog();

            if(result == true) {
                Process.Start(opd.FileName);
            }

        }
        private void OpenDagensSpil_Click(object sender, RoutedEventArgs e) {
            try {
                string filNavn = "/" + dato + ".txt";

                Process.Start(path + filNavn);

            } catch{
                tekstBoxPopUp.Text = "Der er ingen filer fra i dag";
                ShowPopUp();
            }

        }

        private void PuljeSpil_Click(object sender, RoutedEventArgs e) {
            List<int> allePuljetal = new List<int>();
            
            for(int opl = 0; opl<puljeList.Count; opl++) {
                allePuljetal.Add(puljeList[opl]);
            }

            PuljeSpilWindow pSWin = new PuljeSpilWindow();
            pSWin.windowPuljeSpilList = allePuljetal;
            pSWin.Show();

            puljeList.Clear();
            NytSpil();
        }

        private void jackPotSpil_Click(object sender, RoutedEventArgs e) {
            List<int> alleJackpottal = new List<int>();

            for (int opl = 0; opl < jackpotList.Count; opl++) {
                alleJackpottal.Add(jackpotList[opl]);
            }

            JackpotSpil jSWin = new JackpotSpil();
            jSWin.windowJackPotSpilList = alleJackpottal;
            jSWin.Show();

            jackpotList.Clear();
            NytSpil();
        }
        

        //Frem og tilbage knapper
        private void SeTilbage_Click(object sender, RoutedEventArgs e) {
            try {
                int tilbageTal = 0;

                for (int t = 0; t < brugteTalList.Count; t++) {
                    tilbageTal = t;
                }
                talLabel.Content = brugteTalList[tilbageTal - 1];
                
                NyTalKnap.Style = (Style)(this.Resources["DisableKnap"]);

            } catch {
                talLabel.Content = "\"";
            }
            
        }

        private void frem_Click(object sender, RoutedEventArgs e) {
            try {
                int tilbageTal = 0;

                for (int t = 0; t < brugteTalList.Count; t++) {
                    tilbageTal = t;
                }
                talLabel.Content = brugteTalList[tilbageTal];
                NyTalKnap.Style = (Style)(this.Resources["Knapper"]);

            } catch {
                talLabel.Content = "\"";
            }

        }

        private void rowFortryd_Click(object sender, RoutedEventArgs e) {
            try {
                if (puljeList.Contains(Convert.ToInt32(talLabel.Content))) {
                    puljeList.Remove(Convert.ToInt32(talLabel.Content));

                    if (jackpotList.Contains(Convert.ToInt32(talLabel.Content))) {
                        jackpotList.Remove(Convert.ToInt32(talLabel.Content));
                    }
                }
            } catch {
                talLabel.Content = "\"";
            }
        }

        private void FuldFortryd_Click(object sender, RoutedEventArgs e) {
            try {
                if (puljeList.Contains(Convert.ToInt32(talLabel.Content)) && jackpotList.Contains(Convert.ToInt32(talLabel.Content))) {
                    puljeList.Remove(Convert.ToInt32(talLabel.Content));
                    jackpotList.Remove(Convert.ToInt32(talLabel.Content));
                }

            } catch {
                talLabel.Content = "\"";
            }
        }


        //metoder til det bagvedlæggende
        internal void NytTal(object sender, EventArgs e) {//
            try {
                rnd = new Random();
                int Value = rnd.Next(1, talList.Count);
                talLabel.Content = talList[Value];
                brugteTalList.Add(talList[Value]);


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

            } catch {
                talLabel.Content = "\"";
            }
        }

        internal void TalListeGenerator() {
            for (int i = 0; i < 91; i++) {
                talList.Add(i);
            }

        }

        internal void Gemfil() {
            StringBuilder builder = new StringBuilder();
            string result = @"C:\Banko\" + dato + ".txt";

            brugteTalList.Sort();
            foreach (int str in brugteTalList) {
                builder.Append(str.ToString()).AppendLine();
            }

            //laver mappen banko, hvis den ikke findes i forvejen
            if (!Directory.Exists(path)) {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }

            //laver filen for i dag, og hvis den findes så putter den bare mere tekst i filen                
            if (!File.Exists(result)) {
                antalSpil++;
                File.WriteAllText(result, "Spil nr." + antalSpil + " kl." + tid + Environment.NewLine + builder.ToString() + Environment.NewLine);
            } else {
                antalSpil++;
                File.AppendAllText(result, "Spil nr." + antalSpil + " kl." + tid + Environment.NewLine + builder.ToString() + Environment.NewLine);
            }
        }

        internal void TalTilHvid() {
            foreach (UIElement ele in leftWithNumbers.Children) {
                if (ele.GetType() == typeof(Label)) {
                    Label lbl = (Label)ele;
                    lbl.Foreground = Brushes.White;
                    lbl.BorderThickness = new Thickness(1);
                    lbl.FontSize = 25;
                    lbl.BorderBrush = Brushes.Gainsboro;
                }
            }
        }

        internal void NytSpil() {
            Gemfil();
            talList.Clear();

            TalListeGenerator();
            TalTilHvid();
            talLabel.Content = " ";

            brugteTalList.Clear();
        }


        // popUp metoder

        public void ShowPopUp() {
            PopUp.IsOpen = true;

            DispatcherTimer timer = new DispatcherTimer() {
                Interval = TimeSpan.FromSeconds(2)
            };

            timer.Tick += delegate (object sender, EventArgs e)
            {
                ((DispatcherTimer)timer).Stop();
                if (PopUp.IsOpen) PopUp.IsOpen = false;
            };

            timer.Start();
        }

    }
}
 