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
    /// Interaction logic for EditParticipant.xaml
    /// </summary>
    public partial class EditParticipant : Window
    {
        public Participant participant { get; set; }

        public EditParticipant(Participant p)
        {
            InitializeComponent();
            tbEmail.Text = p.GetEmail();
            tbName.Text = p.GetName();
            participant = p;
        }
        //Validating email input
        bool IsValidEmail(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            if(IsValidEmail(tbEmail.Text))
            {
                if (MessageBoxResult.Yes == MessageBox.Show("Are you done?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    participant.SetName(tbName.Text);
                    participant.SetEmail(tbEmail.Text);
                    GlobalConfig.SqlConnection.UpdateParticipant(participant);
                    this.DialogResult = true;
                    this.Close();
                }

            }
            else
            {
                MessageBox.Show("Email address is not in valid format", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

       

        
        
    }
}
