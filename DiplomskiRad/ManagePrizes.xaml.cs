using DiplomskiRad.Classes;
using DiplomskiRad.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Schema;

namespace DiplomskiRad
{
    /// <summary>
    /// Interaction logic for ManagePrizes.xaml
    /// </summary>
    public partial class ManagePrizes : Window
    {
        private int placesPaid;
        private Tournament tournament;
        public ObservableCollection<Prize> prizes { get; set; }
        //True when ManagePrizes window is loaded
        bool loaded = false;

        public ManagePrizes(Tournament t)
        {
            InitializeComponent();
            tournament = t;
            if(tournament.prizes != null)
            {
                prizes = tournament.prizes;
            }
            else
            {
                prizes = new ObservableCollection<Prize>();
            }
            lbPrizes.ItemsSource = prizes;
            loaded = true;

        }
        private void PrizeStructureChanged(object sender, RoutedEventArgs e)
        {
            if (loaded)
            {
                tbPercentage.Clear();
                tbPrizeAmount.Clear();
                if (rbAmount.IsChecked == true)
                {
                    tbPrizeAmount.IsEnabled = true;
                    tbPercentage.IsEnabled = false;
                }
                else
                {
                    tbPrizeAmount.IsEnabled = false;
                    tbPercentage.IsEnabled = true;
                }
            }
        }
        private void AutomaticCheckedChange(object sender, RoutedEventArgs e)
        {
            PrizeGrid.IsEnabled = false;
            tbPlacesPaid.IsEnabled = true;
        }
        private void CustomCheckedChange(object sender, RoutedEventArgs e)
        {
            if(loaded)
            {
                prizes.Clear();
                tbPlacesPaid.Clear();
                PrizeGrid.IsEnabled = true;
                tbPlacesPaid.IsEnabled = false;
            }  
        }

        private static readonly Regex _regex = new Regex("[0-9]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        { 
            return _regex.IsMatch(text);
        }

        private new void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        //Calculating single prize depending on the provided arguments using exponential decay 
        private double CalculatePrize(int numberOfPlayers, int rank, double prizePool, int placesPaid)
        {
            int n = numberOfPlayers;
            int r = rank;
            double l = prizePool;
            double totalPrize = 0;
            double p = 0.7;

            // Calculate the total prize pool using the provided formula for the top 4 ranks
            for (int i = 1; i <= placesPaid; i++)
            {
                totalPrize += ((1 - p) / (1 - Math.Pow(p, n)) * Math.Pow(p, i - 1)) * l;
            }

            // Calculate the individual prize for the given rank
            double individualPrize = ((1 - p) / (1 - Math.Pow(p, n)) * Math.Pow(p, r - 1)) * l;

            // Normalize the prizes so that the sum equals the prize pool
            double normalizationFactor = prizePool / totalPrize;
            double prize = individualPrize * normalizationFactor;

            return prize;
        }

        private void tbPlacesPaid_TextChanged(object sender, TextChangedEventArgs e)
        {
            prizes.Clear();
            float prizePool = tournament.GetNumberOfParticipants() * tournament.entryFee;
            if (!String.IsNullOrEmpty(tbPlacesPaid.Text))
            {
                if(Int32.Parse(tbPlacesPaid.Text) <= tournament.GetNumberOfParticipants())
                {
                    placesPaid = Int32.Parse(tbPlacesPaid.Text);
                    for (int i = 1; i <= placesPaid; i++)
                    {
                        double prizeAmount = CalculatePrize(tournament.GetNumberOfParticipants(), i, prizePool, placesPaid);
                        Prize p = new Prize(i, Math.Round(prizeAmount, 2), (prizeAmount / prizePool) * 100, tournament);
                        prizes.Add(p);
                    }
                }
                else
                {
                    MessageBox.Show("Number of places paid can't be higher than number of participants!", "Notification", MessageBoxButton.OK, MessageBoxImage.Stop);
                    tbPlacesPaid.Clear();
                }         
            }    
        }

        private void btnCreatePrize_Click(object sender, RoutedEventArgs e)
        {
            float prizePool = tournament.GetNumberOfParticipants() * tournament.entryFee;
            //Check if price with the same placeNumber already exists
            if (!String.IsNullOrEmpty(tbPlaceNumber.Text))
            {
                if (prizeExists(int.Parse(tbPlaceNumber.Text), prizes))
                {
                    MessageBox.Show("Prize for that place number already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (rbAmount.IsChecked == true)
            {
                if(!String.IsNullOrEmpty(tbPrizeAmount.Text) && !String.IsNullOrEmpty(tbPlaceNumber.Text))
                {
                    Prize p = new Prize(int.Parse(tbPlaceNumber.Text), Double.Parse(tbPrizeAmount.Text), (Double.Parse(tbPrizeAmount.Text) / prizePool)*100, tournament);
                    prizes.Add(p);
                    var sortedPrizes = new ObservableCollection<Prize>(prizes.OrderBy(prize => prize.placeNumber));
                    prizes = sortedPrizes;
                    tbPrizeAmount.Clear();
                    tbPlaceNumber.Clear();
                    lbPrizes.ItemsSource = prizes;
                }
                else
                {
                    MessageBox.Show("Amount and place number fields can't be empty", "Erorr", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else if(rbPercentage.IsChecked == true)
            {
                if(!String.IsNullOrEmpty(tbPercentage.Text) && !String.IsNullOrEmpty(tbPlaceNumber.Text))
                {
                    Prize p = new Prize(int.Parse(tbPlaceNumber.Text), (Double.Parse(tbPercentage.Text)/100)*prizePool, Double.Parse(tbPercentage.Text), tournament);
                    prizes.Add(p);
                    var sortedPrizes = new ObservableCollection<Prize>(prizes.OrderBy(prize => prize.placeNumber));
                    prizes = sortedPrizes;
                    tbPrizeAmount.Clear();
                    tbPlaceNumber.Clear();
                    lbPrizes.ItemsSource = prizes;
                }
                else
                {
                    MessageBox.Show("Percentage and place number fields can't be empty", "Erorr", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private void btnClearPrizes_Click(object sender, RoutedEventArgs e)
        {
            prizes.Clear();
        }

        private void btnRemovePrize_Click(object sender, RoutedEventArgs e)
        {
            if (lbPrizes.SelectedIndex != -1)
            {
                prizes.Remove((Prize)lbPrizes.SelectedItem);
                var sortedPrizes = new ObservableCollection<Prize>(prizes.OrderBy(prize => prize.placeNumber));
                prizes = sortedPrizes;
                lbPrizes.ItemsSource = prizes;

            }
            else
                MessageBox.Show("You have not selected a prize!", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);         
        }

        private void btnSavePrizes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            GlobalConfig.SqlConnection.DeletePrizes(tournament);
            foreach(Prize p in prizes)
            {
                GlobalConfig.SqlConnection.CreatePrize(p);
            }
            this.Close();    
        }

        private bool prizeExists(int placeNumber, ObservableCollection<Prize> prizes)
        {
            bool exists = prizes.Any(p => p.placeNumber == placeNumber);
            return exists;
        }
    }
}
