using DiplomskiRad.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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
using System.Text.RegularExpressions;
using DiplomskiRad.Database;

namespace DiplomskiRad
{
    /// <summary>
    /// Interaction logic for DodajTakmicara.xaml
    /// </summary>
    public partial class AddParticipant : Window
    {
        public ObservableCollection<Participant> participants;
        public AddParticipant(ObservableCollection<Participant> p)
        {
            InitializeComponent();
            participants = p;
            ObservableCollection<Participant> allParticipants = GlobalConfig.SqlConnection.SelectParticipants();
            var sortedParticipants = new ObservableCollection<Participant>(allParticipants.OrderBy(p => p.Name));
            cbParticipants.ItemsSource = sortedParticipants;

        }

        public Participant participant;
       
        //Validating email input
        bool IsValidEmail(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }

        // Method for adding new individual participant to the tournamnet
        private void AddPart(object sender, RoutedEventArgs e)
        {

            //Check if input fields are fullfiled
            if (!String.IsNullOrEmpty(tbName.Text) && !String.IsNullOrEmpty(tbEmail.Text))
            {

                if (IsValidEmail(tbEmail.Text))
                {

                    if (ParticipantExists(participants, tbEmail.Text))
                    {
                        MessageBox.Show("Participant with this email already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        participant = new Participant(tbName.Text, tbEmail.Text);
                        this.DialogResult = true;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Email address is not in valid format", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("You have to fulfill all the input fields!", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void SelectPart(object sender, RoutedEventArgs e)
        {
            if (cbParticipants.SelectedIndex != -1)
            {
                participant = (Participant)cbParticipants.SelectedItem;
                if (ParticipantExists(participants, participant.Email))
                {
                    MessageBox.Show("Participant with this email already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    this.DialogResult = true;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("You have not selected a participant!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Check if Participant with same email already exists in a list of participants
        private bool ParticipantExists(ObservableCollection<Participant> participants, String email)
        {
            foreach (Participant p in participants)
            {
                if (p.Email == email)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

