using DiplomskiRad.Classes;
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
        Takmicenje tournament;
 
        public MainWindow(Takmicenje t)
        {
            
            InitializeComponent();
            tournament = t;
            this.Title = "Organizacija takmicenja - " + t.GetNazivTakmicenja();
            MinWidth = 1440;
            MinHeight = 920;
            MaxWidth = 1440;
            MaxHeight = 920;
            tbNaslov.Text = t.GetNazivTakmicenja().ToString();
            lbUcesnici.ItemsSource = tournament.GetUcesnici();
            GenerateTakmicenjeInfoString(t);
            
        }

        #region Methods
       
        //Generate tournament info string which will be printed in the textblock
        public void GenerateTakmicenjeInfoString(Takmicenje t)
        {
            String tourInfo = "";

            tourInfo += "Datum početka: " + t.GetDate().ToShortDateString() + "\n\n"
                     + "Tip takmičenja: " + t.GetTIpTakmicenja() + "\n\n" +
                       "Maksimalan broj ekipa: " + t.GetBrojUcesnika() + "\n\n";

            tbInfo.Text = tourInfo;
        }

       
        //Check type of the tournament and call specific method for adding player/team
        private void DodajUcesnika(object sender, RoutedEventArgs e)
        {

            if (tournament.GetTIpTakmicenja() == "Individualno")
                DodajTakmicara();   
            else
                DodajEkipu();
            countTextBlock.Text = $"Number of participants:{tournament.GetNumOfRegisteredParticipants()}"; 
        }
        //Adding player to the tournament
        public void DodajTakmicara()
        {       
            DodajTakmicara dodaj = new DodajTakmicara(tournament.GetUcesnici());
            if (dodaj.ShowDialog() == true)
            {
                Ucesnik ucesnik = new Takmicar(dodaj.GetNazivTakmicara(), dodaj.GetJMBG());
                if(tournament.DodajUcesnika(ucesnik) == false)
                {
                    MessageBox.Show("Kapacitet takmičenja je popunjen!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                
            }
        }
        //Adding team for to the tournament
        public void DodajEkipu()
        {
            DodajEkipu dodaj = new DodajEkipu(tournament.GetUcesnici());
            if(dodaj.ShowDialog() == true)
            {
                if(tournament.DodajUcesnika(dodaj.ekipa) == false)
                {
                    MessageBox.Show("Kapacitet takmičenja je popunjen!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
          
        }
        //Calling function to construct bracket and send message to the user 
        private void KreirajZreb(object sender, RoutedEventArgs e)
        {
            if (tournament.KonstruisiZreb())
            {
                MessageBox.Show("Uspešno kreiran žreb!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                btnCreateBracket.Visibility = Visibility.Collapsed;
                btnDeleteParticipant.Visibility = Visibility.Collapsed;
                btnAddParticipant.Visibility = Visibility.Collapsed;
                btnEditParticipant.Visibility = Visibility.Collapsed;  
                btnResetBrascket.Visibility = Visibility.Visible;
                BracketTreeView.ItemsSource = tournament.zreb.listaKola;
            }
            else
            {
                MessageBox.Show("Broj učesnika ne ispunjava uslove takmičenja!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Stop);
            }

        }

        private void WinnerSelected(object sender, RoutedEventArgs e)
        {

            RadioButton rb = (RadioButton)sender;
            int matchID = int.Parse(rb.GroupName);
            Ucesnik u = (Ucesnik)rb.Tag;
            int nextGameID = 0;

            //Looking for a match with mecID as matchID and adding the winner
            for (int i = 0; i < tournament.zreb.listaKola.Count(); i++)
            { 
                for (int j = 0; j < tournament.zreb.listaKola[i].mecevi.Count(); j++)
                {
                    if (tournament.zreb.listaKola[i].mecevi[j].mecID == matchID)
                    {
                        tournament.zreb.listaKola[i].mecevi[j].unesiPobednika(u);
                        nextGameID = tournament.zreb.listaKola[i].mecevi[j].nextGame;
                        break;
                    }
                }
            }

            //Looking for a next match of the winner and updating the participants
            for (int i = 0; i < tournament.zreb.listaKola.Count(); i++)
            {
                for (int j = 0; j < tournament.zreb.listaKola[i].mecevi.Count(); j++)
                {
                    if (tournament.zreb.listaKola[i].mecevi[j].mecID == nextGameID)
                    {
                        tournament.zreb.listaKola[i].mecevi[j].updateUcesnik();
                        break;
                    }
                }
            }
        }

        //Removing selected participant from the tournament
        private void RemoveParticipant(object sender, RoutedEventArgs e)
        {
            if (lbUcesnici.SelectedIndex != -1)
            {
                Takmicar t = (Takmicar)lbUcesnici.SelectedItem;
                if (MessageBox.Show("Are you sure you want to delete user:\n" + "Name: " + t.GetNazivUcesnika() + "\n" + "JMBG: " + t.GetJmbg(), "Requesting confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    tournament.RemoveParticipant((Ucesnik)lbUcesnici.SelectedItem);
                    MessageBox.Show("The participant has been successfully deleted", "Participant deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    countTextBlock.Text = $"Number of participants:{tournament.GetNumOfRegisteredParticipants()}";
                }
               
            }
            else
                MessageBox.Show("You have not selected a participant!", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void ResetBracket(object sender, RoutedEventArgs e)
        {
            tournament.ResetBracket();
            btnResetBrascket.Visibility = Visibility.Collapsed; 
            btnCreateBracket.Visibility = Visibility.Visible;
            btnDeleteParticipant.Visibility = Visibility.Visible;
            btnAddParticipant.Visibility = Visibility.Visible;
            btnEditParticipant.Visibility = Visibility.Visible;
        }
        private void EditParticipant(object sender, RoutedEventArgs e)
        {
            if(lbUcesnici.SelectedIndex != -1)
            {
                Takmicar participant = (Takmicar)lbUcesnici.SelectedItem;
                int selectedIndex = lbUcesnici.SelectedIndex;
                EditParticipant editWindow = new EditParticipant(participant);
                if (editWindow.ShowDialog() == true)
                {
                    Takmicar editedParticipant = editWindow.participant;
                    tournament.SetSpecificParticipant(selectedIndex, editedParticipant);
                    lbUcesnici.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("You have not selected any participant", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        #endregion

       
    }
}
