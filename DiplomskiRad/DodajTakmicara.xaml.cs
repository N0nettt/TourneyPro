using DiplomskiRad.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DiplomskiRad
{
    /// <summary>
    /// Interaction logic for DodajTakmicara.xaml
    /// </summary>
    public partial class DodajTakmicara : Window
    {
        public ObservableCollection<Ucesnik> ucesnici;
        public DodajTakmicara(ObservableCollection<Ucesnik> u)
        {
            InitializeComponent();
            ucesnici = u;    
        }

        public string JMBG = "";
        public string nazivTakmicara = "";

        public string GetJMBG()
        {
            return JMBG;
        }
        public string GetNazivTakmicara()
        {
            return nazivTakmicara;
        }

        // Method for adding new individual participant to the tournamnet
        private void DodajUcesnika(object sender, RoutedEventArgs e)
        {
            //Check if input fields are fullfiled
            if (!String.IsNullOrEmpty(tbNazivTakmicara.Text) && !String.IsNullOrEmpty(tbJMBG.Text))
            {
                if(Int64.TryParse(tbJMBG.Text, out long result)) 
                {
                    //Check if JMBG has exactly 13 numbers in order to add new participant
                    if (tbJMBG.Text.Length != 13)
                    {
                        bool exists = false;
                        //Foreach method which goes through list and checks if player with inputed JMBG exists
                        foreach(Ucesnik u in ucesnici)
                        {
                            var a = ((Takmicar)u).GetJmbg();
                            if(a == tbJMBG.Text)
                            {
                                exists = true;
                            }
                        }                            
                        if(exists == true)
                        {
                            MessageBox.Show("Takmičar sa ovim JMBG-om već postoji!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else
                        {
                            nazivTakmicara = tbNazivTakmicara.Text;
                            JMBG = tbJMBG.Text;
                            this.DialogResult = true;
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("JMBG mora da sadrži tačno 13 cifara!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("JMBG može da sadrži samo brojeve!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Morate uneti sve informacije o takmičaru!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }
    }
}

