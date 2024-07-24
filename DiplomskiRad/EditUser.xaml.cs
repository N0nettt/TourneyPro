using DiplomskiRad.Classes;
using DiplomskiRad.Database;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace DiplomskiRad
{
    /// <summary>
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        public User user;
        public EditUser(User u)
        {
            InitializeComponent();
            user = u;
            tbEmail.Text = user.Email;
            cbRoles.ItemsSource = GlobalConfig.SqlConnection.SelectRoles();
            cbRoles.SelectedValue = u.Role.RoleID;
        }

        private bool ValidateInputs(string email, string username)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateInputs(tbEmail.Text, tbUsername.Text))
            {
                if (MessageBoxResult.Yes == MessageBox.Show("Are you done", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    user.Email = tbEmail.Text;
                    user.Username = tbUsername.Text;
                    user.Role.RoleID = ((Role)cbRoles.SelectedItem).RoleID;
                    GlobalConfig.SqlConnection.UpdateUser(user);
                    this.DialogResult = true;
                    this.Close();
                }
            }
        }

        private void tbUsername_Loaded(object sender, RoutedEventArgs e)
        {
            tbUsername.Text = user.Username;
        }
    }
}
