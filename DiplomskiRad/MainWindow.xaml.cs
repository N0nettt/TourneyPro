using DiplomskiRad.Classes;
using DiplomskiRad.Database;
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
using System.Windows.Shapes;

namespace DiplomskiRad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Tournament tournament;
        User user = SessionManager.LoggedInUser;
 
        public MainWindow(Tournament t)
        {
            
            InitializeComponent();
            tournament = t;
            this.Title = "Tournament organization - " + t.GetTournamentName();
            MinWidth = 1440;
            MinHeight = 920;
            MaxWidth = 1440;
            MaxHeight = 920;
            tbTitle.Text = t.GetTournamentName().ToString();
            lbParticipats.ItemsSource = tournament.GetParticipants();
            tournament.WinnerSelected += OnWinnerAnnounced;
            GetTournamentInfoString(t);
            if (tournament.GetManagePayouts())
            {
                lbPrizes.Visibility = Visibility.Visible;
                btnManagePrizes.Visibility = Visibility.Visible;
                labelPrizes.Visibility = Visibility.Visible;
            }
            if(tournament.bracket.listOfRounds.Count > 0)
            {
                BracketCreatedButtons();
            }
            if(user.Role.RoleName == "Korisnik")
            {
                HideAllbuttons();
            }
            lbPrizes.ItemsSource = tournament.prizes;
            BracketTreeView.ItemsSource = tournament.bracket.listOfRounds;
        }

        #region Methods
        private void OnWinnerAnnounced()
        {
            // Call the method to update tbInfo.Text with the tournament info
            GetTournamentInfoString(tournament);
        }

        //Generate tournament info string which will be printed in the textblock
        public void GetTournamentInfoString(Tournament t)
        {
            String tourInfo = "";
            String winner = "";
            if (t.winner != null) { winner = t.winner.GetName(); }

            tourInfo += "Date of beginning: " + t.GetDate().ToShortDateString() + "\n\n"
                     + "Max number of participants: " + t.GetNumberOfParticipants() + "\n\n"
                     + "Winner: " + winner;

            tbInfo.Text = tourInfo;
        }



        //Adding player to the tournament
        private void AddParticipant(object sender, RoutedEventArgs e)
        {

            AddParticipant add = new AddParticipant(tournament.GetParticipants());
            if (add.ShowDialog() == true)
            {
                Participant p = add.participant;
                if (tournament.AddParticipant(p) == false)
                {
                    MessageBox.Show("Capacity of the tournamnet is full!", "Notification", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                countTextBlock.Text = $"Number of participants:{tournament.GetNumOfRegisteredParticipants()}";

            }     
        }
        
       
        //Calling function to construct bracket and send message to the user 
        private void CreateBracket(object sender, RoutedEventArgs e)
        {
            if (tournament.ConstructBracket())
            {
                MessageBox.Show("Successfully created bracket!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                BracketCreatedButtons();
                BracketTreeView.ItemsSource = tournament.bracket.listOfRounds;
            }
            else
            {
                MessageBox.Show("The number of participants does not meet the competition requirements", "notification", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
        // Hide/Show different buttons if bracket is created
        private void BracketCreatedButtons()
        {
            btnCreateBracket.Visibility = Visibility.Collapsed;
            btnDeleteParticipant.Visibility = Visibility.Collapsed;
            btnAddParticipant.Visibility = Visibility.Collapsed;
            btnEditParticipant.Visibility = Visibility.Collapsed;
            btnResetBrascket.Visibility = Visibility.Visible;
        }
        // Hide all buttons if normal user log in
        private void HideAllbuttons()
        {
            btnCreateBracket.Visibility = Visibility.Collapsed;
            btnDeleteParticipant.Visibility = Visibility.Collapsed;
            btnAddParticipant.Visibility = Visibility.Collapsed;
            btnEditParticipant.Visibility = Visibility.Collapsed;
            btnManagePrizes.Visibility = Visibility.Collapsed;
            btnResetBrascket.Visibility = Visibility.Collapsed;
            
        }

        private void WinnerSelected(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (user.Role.RoleName == "Korisnik")
            {
                MessageBox.Show("User can not select the winner!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                rb.IsChecked = false;
                return;
            }

            
            int matchID = int.Parse(rb.GroupName);
            Participant p = (Participant)rb.Tag;
            int nextGameID = 0;

            //Looking for a match with mecID as matchID and adding the winner
            for (int i = 0; i < tournament.bracket.listOfRounds.Count(); i++)
            { 
                for (int j = 0; j < tournament.bracket.listOfRounds[i].matches.Count(); j++)
                {
                    if (tournament.bracket.listOfRounds[i].matches[j].MatchID == matchID)
                    {
                        tournament.bracket.listOfRounds[i].matches[j].SetWinner(p);
                        nextGameID = tournament.bracket.listOfRounds[i].matches[j].NextMatch;
                        break;
                    }
                }
            }
            //Looking for a next match of the winner and updating the participants
            for (int i = 0; i < tournament.bracket.listOfRounds.Count(); i++)
            {
                for (int j = 0; j < tournament.bracket.listOfRounds[i].matches.Count(); j++)
                {
                    if (tournament.bracket.listOfRounds[i].matches[j].MatchID == nextGameID)
                    {
                        tournament.bracket.listOfRounds[i].matches[j].updateUcesnik();
                        break;
                    }
                }
            }
        }
        //Removing selected participant from the tournament
        private void RemoveParticipant(object sender, RoutedEventArgs e)
        {
            if (lbParticipats.SelectedIndex != -1)
            {
                Participant p = (Participant)lbParticipats.SelectedItem;
                if (MessageBox.Show("Are you sure you want to remove Participant:\n" + "Name: " + p.GetName() + "\n" + "Email: " + p.GetEmail(), "Requesting confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    tournament.RemoveParticipant((Participant)lbParticipats.SelectedItem);
                    MessageBox.Show("The participant has been successfully deleted", "Participant deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    countTextBlock.Text = $"Number of participants:{tournament.GetNumOfRegisteredParticipants()}";
                }
               
            }
            else
                MessageBox.Show("You have not selected a participant!", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void ResetBracket(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Do you want to reset the bracket?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(result == MessageBoxResult.Yes)
            {
                tournament.ResetBracket();
                btnResetBrascket.Visibility = Visibility.Collapsed;
                btnCreateBracket.Visibility = Visibility.Visible;
                btnDeleteParticipant.Visibility = Visibility.Visible;
                btnAddParticipant.Visibility = Visibility.Visible;
                btnEditParticipant.Visibility = Visibility.Visible;
            }
        }
        private void EditParticipant(object sender, RoutedEventArgs e)
        {
            if(lbParticipats.SelectedIndex != -1)
            {
                Participant participant = (Participant)lbParticipats.SelectedItem;
                int selectedIndex = lbParticipats.SelectedIndex;
                EditParticipant editWindow = new EditParticipant(participant);
                if (editWindow.ShowDialog() == true)
                {
                    Participant editedParticipant = editWindow.participant;
                    tournament.SetSpecificParticipant(selectedIndex, editedParticipant);
                    GlobalConfig.SqlConnection.UpdateParticipant(editedParticipant);
                    lbParticipats.Items.Refresh();
                    BracketTreeView.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("You have not selected a participant!", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ManagePrizes(object sender, RoutedEventArgs e)
        {
            ManagePrizes managePrizes = new ManagePrizes(tournament);
            if (managePrizes.ShowDialog() == true)
            {
                tournament.prizes = managePrizes.prizes;
                lbPrizes.ItemsSource = tournament.prizes;
            }
        }

        #endregion

        private void btnEditTournament_Click(object sender, RoutedEventArgs e)
        {
            EditTournament editWindow = new EditTournament(tournament);
            if(editWindow.ShowDialog() == true)
            {
                tournament = editWindow.tournament;
                tbTitle.Text = tournament.tournamentName;
                GetTournamentInfoString(tournament);
                if (tournament.managePayouts == false)
                {
                    btnManagePrizes.Visibility = Visibility.Collapsed;
                    lbPrizes.Visibility = Visibility.Collapsed;
                    labelPrizes.Visibility = Visibility.Visible;
                }
                else
                {
                    btnManagePrizes.Visibility = Visibility.Visible;
                    lbPrizes.Visibility = Visibility.Visible;
                }
                GlobalConfig.SqlConnection.UpdateTournament(tournament);
            }
        }
    }
}
