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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            DateTime date = DateTime.Now;
            datum.SelectedDate = date;
            datum.DisplayDateStart = DateTime.Today;
        }

        //Creating new tournament on a button click
        private void btnKreirajTakmicenje_Click(object sender, RoutedEventArgs e)
        {
            //Checking if every information is given
            if(!String.IsNullOrEmpty(tbNazivTakmicenja.Text) && cbBrojUcesnika.SelectedIndex != -1 && (rbEkipno.IsChecked == true || rbIndividualno.IsChecked == true))
            {
               DateTime date = (DateTime)datum.SelectedDate;
               String tip = "";
               int brojUcensika;
               int.TryParse(cbBrojUcesnika.Text, out brojUcensika);
               if(rbEkipno.IsChecked == true)
               {
                    tip = "Ekipno"; 
               }
               else
               {
                    tip = "Individualno";
               }
               Takmicenje takmicenje = new Takmicenje(tbNazivTakmicenja.Text, date, brojUcensika, tip);
               MainWindow main = new MainWindow(takmicenje);
               this.Close();            
               main.Show();
                 
                
            }
            else
            {
                MessageBox.Show("Morate uneti sve informacije o takmičenju", "Obaveštenje");
            }
        }
    }
}
