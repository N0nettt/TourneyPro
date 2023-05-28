using DiplomskiRad.Classes;
using System;
using System.Collections.Generic;
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
        public List<Ucesnik> ucesnici;
        public DodajTakmicara(List<Ucesnik> u)
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

        private void DodajUcesnika(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tbNazivTakmicara.Text) && !String.IsNullOrEmpty(tbJMBG.Text))
            {
                if(Int64.TryParse(tbJMBG.Text, out long result)) 
                {
                    if (tbJMBG.Text.Length == 13)
                    {

                        bool exists = false;
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
                            MessageBox.Show("Takmičar sa ovim JMBG-om već postoji!");
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
                        MessageBox.Show("JMBG mora da sadrži tačno 13 cifara!");
                    }
                }
                else
                {
                    MessageBox.Show("JMBG može da sadrži samo brojeve!");
                }

            }
            else
            {
                MessageBox.Show("Morate uneti sve informacije o takmičaru!", "Obaveštenje");

            }

        }
    }
}

