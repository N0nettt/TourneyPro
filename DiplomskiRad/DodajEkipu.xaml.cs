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
    /// Interaction logic for DodajEkipu.xaml
    /// </summary>
    public partial class DodajEkipu : Window
    {
        public List<Ucesnik> ucesnici { get; set; }
        public Ekipa ekipa = new Ekipa("naziv");
        private List<Takmicar> takmicari { get; set; }
        public DodajEkipu(List<Ucesnik> u)
        {
            InitializeComponent();
            ucesnici = u;
            takmicari = new List<Takmicar>();         
        }

        // Method which adds a Takmicar in the team 
         private void DodajTakmičara(object sender, RoutedEventArgs e)
        {
            DodajTakmicara dodaj = new DodajTakmicara(ucesnici);
            if (dodaj.ShowDialog() == true)
            {
                lbClanoviEkipe.ItemsSource = null;
                Takmicar ucesnik = new Takmicar(dodaj.GetNazivTakmicara(), dodaj.GetJMBG());
                ekipa = TakmicarExists(ekipa, ucesnik);
                takmicari = ekipa.GetTakmicari();   
                lbClanoviEkipe.ItemsSource = takmicari;
            }
        }

        private void KreirajTeam(object sender, RoutedEventArgs e)
        {
            bool nameExists = ucesnici.Any(u => u.GetNazivUcesnika() == tbNazivEkipe.Text);
            if (nameExists)
            {
                MessageBox.Show("Ekipa sa ovim imenom već postoji, molimo vas promenite ime!");
                
            }
            else
            {
                ekipa.SetNazivUcesnika(tbNazivEkipe.Text);
                this.DialogResult = true;
                this.Close();
            }
        }

        //Check if Takmicar exists in the ekipa or in the tournament and adds if not
        private Ekipa TakmicarExists(Ekipa ekipa, Takmicar takmicar)
        {
            List<Takmicar> listaIgraca = ekipa.GetTakmicari();
            // Check if a player with the ID we want to create already exist in the team we are creating show msg if do
            bool existsInTeam = listaIgraca.Any(tak => tak.jmbg == takmicar.jmbg);
            if (existsInTeam)
            {
                MessageBox.Show("Igrač sa tim JMBG-om već posotji", "Obaveštenje");
                return ekipa;
            }
            bool existsInTournament = false;
            String teamName = "";
            String playerName = "";
            // Check if a player with the ID we want to create already exist in a team in the tournament and show msg if do
            foreach (Ekipa e in ucesnici)
            {
                foreach(Takmicar t in e.takmicari)
                {
                    if (t.GetJmbg == takmicar.GetJmbg)
                    {
                        existsInTournament = true;
                        teamName = e.GetNazivUcesnika();
                        playerName = t.GetNazivUcesnika();
                    }
                }
            }
            if(existsInTournament)
            {
                MessageBox.Show("Igrač sa tim JMBG-om već postoji u ekipi: " + teamName + ", pod nazivom: " + playerName);
                return ekipa;
            }
            // Add a player to the team if it doesnt exist already in a team/tournament
            else
            {
                ekipa.DodajTakmicara(takmicar);
                return ekipa;
            }
            
            
            
        }

        
    }
}
