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
    /// Interaction logic for UserSelectTournamentWindow.xaml
    /// </summary>
    public partial class UserSelectTournamentWindow : Window
    {
        public UserSelectTournamentWindow()
        {
            InitializeComponent();
            User user = SessionManager.LoggedInUser;
            ObservableCollection<Tournament> tournaments = new ObservableCollection<Tournament>();
            tournaments = GlobalConfig.SqlConnection.SelectParticipantTournaments(user);
            cbTournaments.ItemsSource = tournaments;
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
    }
}
