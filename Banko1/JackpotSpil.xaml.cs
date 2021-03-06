﻿using System;
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
using System.Windows.Shapes;

namespace Banko1 {
    /// <summary>
    /// Interaction logic for JackpotSpil.xaml
    /// </summary>
    public partial class JackpotSpil : Window {
        internal List<int> talList = new List<int>();
        internal List<int> brugteTalList = new List<int>();
        internal int antalSpil = 0;

        public JackpotSpil() {
            InitializeComponent();

            TalTilHvid();

            TalListeGenerator();

        }

        public List<int> windowJackPotSpilList {
            get;
            set;
        }

        //klik event metoder
        internal void NyTal_Click(object sender, RoutedEventArgs e) {
            try {
                NytTal();

            } catch {
                talLabel.Content = "\"";
            }
        }


        //Frem og tilbage knapper
        private void SeTilbage_Click(object sender, RoutedEventArgs e) {
            try {
                int tilbageTal = 0;

                for (int t = 0; t < brugteTalList.Count; t++) {
                    tilbageTal = t;
                }
                talLabel.Content = brugteTalList[tilbageTal - 1];

                NyTalKnap.IsEnabled = false;
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

                NyTalKnap.IsEnabled = true;
                NyTalKnap.Style = (Style)(this.Resources["Knapper"]);

            } catch {
                talLabel.Content = "\"";
            }

        }


        //metoder til det bagvedlæggende
        internal void NytTal() {
            Random rnd = new Random();

            int Value = rnd.Next(1, talList.Count);
            talLabel.Content = talList[Value];
            brugteTalList.Add(talList[Value]);

            foreach (UIElement ele in gridForTal.Children) {
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
            foreach (UIElement ele in gridForTal.Children) {
                if (ele.GetType() == typeof(Label)) {
                    Label lbl = (Label)ele;
                    lbl.Style = (Style)(this.Resources["talLabelsStyle"]);
                }
            }
        }
    }
}
