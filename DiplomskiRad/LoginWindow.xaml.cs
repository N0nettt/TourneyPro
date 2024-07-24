using DiplomskiRad.Classes;
using DiplomskiRad;
using System.Windows;
using System;
using DiplomskiRad.Database;

namespace DiplomskiRad
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        // Handling Login logic
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(tbUsername.Text) && !String.IsNullOrEmpty(pbPassword.Password))
            {
                string username = tbUsername.Text;
                string password = pbPassword.Password;

                try
                {
                    User loggedUser = GlobalConfig.SqlConnection.LoginUser(username, password);
                    if (loggedUser != null)
                    {
                        SessionManager.SetSession(loggedUser);
                        
                        if (loggedUser.Role.RoleName == "Korisnik")
                        {
                            UserSelectTournamentWindow usrWindow = new UserSelectTournamentWindow();
                            usrWindow.Show();
                            this.Close();
                        }
                        else if (loggedUser.Role.RoleName == "Admin")
                        {
                            Window1 adminWindow = new Window1();
                            adminWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            Window1 organizerWindow = new Window1();
                            organizerWindow.Show();
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("You need to fullfil username nad passwod fields", "Login error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }  
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            // Handle navigation to registration page or show registration dialog
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }

    }
}
