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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        bool loaded = false;
        DateTime date;
        User user = SessionManager.LoggedInUser;
        ObservableCollection<Tournament> tournaments = new ObservableCollection<Tournament>();
        ObservableCollection<User> users = new ObservableCollection<User>();    
        public Window1()
        {
            InitializeComponent();
            date = DateTime.Now;
            datum.SelectedDate = date;
            datum.DisplayDateStart = DateTime.Today;
            tbEntryFee.Visibility = Visibility.Collapsed;
            lbEntryFee.Visibility = Visibility.Collapsed; 
            tournaments = GlobalConfig.SqlConnection.SelectTournamentsByOrganizer(user);
            cbTournaments.ItemsSource = tournaments;
            loaded = true;
            users = GlobalConfig.SqlConnection.SelectUsers();
            lbUsers.ItemsSource = users;
            ShowAdminControls();
        }
        
        private int managePayouts()
        {
            if (cbManagePayouts.IsChecked == true)
            {
                return 1;
            }

            return 0;
        }
        
        private void ShowAdminControls()
        {
            if(user.Role.RoleName == "Admin")
            {
                lbUsers.Visibility = Visibility.Visible;
                btnEditUser.Visibility = Visibility.Visible;
                btnDeleteUser.Visibility = Visibility.Visible;
            }
        }

        //Creating new tournament on a button click
        private void btnCreateTournament_Click(object sender, RoutedEventArgs e)
        {
            
            //Checking if every information is inputed by a user
            if(!String.IsNullOrEmpty(tbTournamentName.Text) && cbNumberOfParticipants.SelectedIndex != -1)
            {
                int fee = 0;
                if(managePayouts() == 1)
                {
                    if (!String.IsNullOrEmpty(tbEntryFee.Text)){
                        if (!Int32.TryParse(tbEntryFee.Text, out int entryfee))
                        {
                            MessageBox.Show("Entry fee has to be decimal number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        else
                        {
                            fee = entryfee;
                        }
                    }
                    else
                    {
                        MessageBox.Show("You have to fulfill every field!", "Notification", MessageBoxButton.OK, MessageBoxImage.Stop);
                        return;
                    }

                }
                date = (DateTime)datum.SelectedDate;
                int numOfPart;
                int.TryParse(cbNumberOfParticipants.Text, out numOfPart);
                Tournament tournament = new Tournament(tbTournamentName.Text, date, numOfPart, managePayouts(), fee, SessionManager.LoggedInUser);
                tournament = GlobalConfig.SqlConnection.CreateTournament(tournament);
                tournament.bracket.BracketID = GlobalConfig.SqlConnection.CreateBracket(tournament.bracket);
                MainWindow main = new MainWindow(tournament);           
                main.Show();
                this.Close();
                    
            }
            else
            {
                MessageBox.Show("You have to fulfill every field!", "Notification", MessageBoxButton.OK, MessageBoxImage.Stop);
                
            }
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
        private void btnOpenTournament_Click(object sender, RoutedEventArgs e)
        {
            if (cbTournaments.SelectedIndex != -1)
            {
                Tournament tournament = (Tournament)cbTournaments.SelectedItem;
                MainWindow mainWindow = new MainWindow(GlobalConfig.SqlConnection.LoadTournament(tournament));
                mainWindow.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("You have not selected a tournament!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteTournament_Click(object sender, RoutedEventArgs e)
        {
            if(cbTournaments.SelectedIndex != -1)
            {
                if (MessageBox.Show("Are you sure you want to delete the tournament?", "Requesting confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Tournament tournament = (Tournament)cbTournaments.SelectedItem;
                    GlobalConfig.SqlConnection.DeleteTournament(tournament);
                    tournaments.Remove(tournament);
                    MessageBox.Show("The tournament has been successfully deleted", "Tournament Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    cbTournaments.Items.Refresh();

                }
            }
            else
            {
                MessageBox.Show("You have not selected a tournament!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            if(lbUsers.SelectedIndex != -1)
            {
                User user = (User)lbUsers.SelectedItem;
                EditUser editUserWindow = new EditUser(user);
                if(editUserWindow.ShowDialog() == true)
                {
                    user = editUserWindow.user;
                    lbUsers.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("You have not selected a User!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedIndex != -1)
            {
                if (MessageBox.Show("Are you sure you want to remove Participant:\n" + "Name: " + user.Username + "\n" + "Email: " + user.Email, "Requesting confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    User user = (User)lbUsers.SelectedItem;
                    GlobalConfig.SqlConnection.DeleteUser(user);
                    users.Remove(user);
                    lbUsers.Items.Refresh();
                    MessageBox.Show("The User has been successfully deleted", "User deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("You have not selected a User!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
    }
}
