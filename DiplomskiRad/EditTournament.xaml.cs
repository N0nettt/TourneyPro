using DiplomskiRad.Classes;
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
using System.Windows.Shapes;

namespace DiplomskiRad
{
    /// <summary>
    /// Interaction logic for EditTournament.xaml
    /// </summary>
    public partial class EditTournament : Window
    {
        private DateTime date;
        private bool loaded = false;
        public Tournament tournament;
        public EditTournament(Tournament t)
        {
            InitializeComponent();
            datum.SelectedDate = t.date;
            datum.DisplayDateStart = DateTime.Today;
            loaded = true;
            tournament = t;
            tbTournamentName.Text = tournament.tournamentName;
            tbEntryFee.Text = tournament.entryFee.ToString();
            ChangeNumberOfParticipants(cbNumberOfParticipants, tournament.numberOfParticipants);
            if (tournament.managePayouts)
            {
                cbManagePayouts.IsChecked = true;
            }
            else
            {
                lbEntryFee.Visibility = Visibility.Collapsed;
                tbEntryFee.Visibility = Visibility.Collapsed;
            }
        }

        private void ChangeNumberOfParticipants(ComboBox cb, int numOfPart)
        {
            switch (numOfPart)
            {
                case 4:
                    cb.SelectedItem = cbNumberOfParticipants.Items[0];
                    break;
                case 8:
                    cb.SelectedItem = cbNumberOfParticipants.Items[1];
                    break;
                case 16:
                    cb.SelectedItem = cbNumberOfParticipants.Items[2];
                    break;
                case 32:
                    cb.SelectedItem = cbNumberOfParticipants.Items[3];
                    break;
                case 64:
                    cb.SelectedItem = cbNumberOfParticipants.Items[4];
                    break;
                default:
                    cb.SelectedIndex = -1; // No selection
                    break;
            }
        }

        private void btnEditTournament_Click(object sender, RoutedEventArgs e)
        {
            if (InputValidations(tournament))
            {
                tournament.tournamentName = tbTournamentName.Text;
                tournament.numberOfParticipants = int.Parse(cbNumberOfParticipants.Text);
                tournament.date = (DateTime)datum.SelectedDate;
                if(cbManagePayouts.IsChecked == true) { tournament.managePayouts = true; tournament.entryFee = float.Parse(tbEntryFee.Text); } else { tournament.managePayouts = false;}
                this.DialogResult = true;
                this.Close();
            }
        }

        private bool InputValidations(Tournament t)
        {
            if (String.IsNullOrEmpty(tbTournamentName.Text))
            {
                MessageBox.Show("Tournament name can't be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            ComboBoxItem selectedItem = (ComboBoxItem)cbNumberOfParticipants.SelectedItem;
            int numOfPart = int.Parse(selectedItem.Content.ToString());
            if(numOfPart < t.participants.Count)
            {
                MessageBox.Show("You can not change maximum number of participants to the number less than number of already added participants!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(datum.SelectedDate != t.date && t.bracket.listOfRounds.Count > 0)
            {
                MessageBox.Show("You can not change the date for already started tournament!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;

            }
            if(cbManagePayouts.IsChecked == true && String.IsNullOrEmpty(tbEntryFee.Text))
            {
                MessageBox.Show("Entry fee can not be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            } 
            if(cbManagePayouts.IsChecked == true && !Int32.TryParse(tbEntryFee.Text, out int entryfee))
            {
                MessageBox.Show("Entry fee has to be decimal number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }


            return true;
        }

        private void cbManagePayouts_Checked(object sender, RoutedEventArgs e)
        {
            if (loaded)
            {
                if (cbManagePayouts.IsChecked == true)
                {
                    lbEntryFee.Visibility = Visibility.Visible;
                    tbEntryFee.Visibility = Visibility.Visible;
                }
                else
                {
                    lbEntryFee.Visibility = Visibility.Collapsed;
                    tbEntryFee.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();

        }
    }
}
