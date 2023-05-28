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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Takmicenje takmicenje;
        private bool m_isDuringMaximizing;

        public List<Ucesnik> ucesnici { get; set; }
        
        public MainWindow(Takmicenje t)
        {
            
            InitializeComponent();
            takmicenje = t;
            this.Title = "Organizacija takmicenja - " + t.GetNazivTakmicenja();
            MinWidth = 1440;
            MinHeight = 920;
            MaxWidth = 1440;
            MaxHeight = 920;
            ucesnici = new List<Ucesnik>();
            tbNaslov.Text = t.GetNazivTakmicenja().ToString(); 
            GenerateTakmicenjeInfoString(t); //Printing tournament info

           
        }

        #region Methods
        //Allowing only maximize and minimze and not manual changing of window size
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                MinWidth = 0;
                MinHeight = 0;
                MaxWidth = int.MaxValue;
                MaxHeight = int.MaxValue;

                if (!m_isDuringMaximizing)
                {
                    m_isDuringMaximizing = true;
                    WindowState = WindowState.Normal;
                    WindowState = WindowState.Maximized;
                    m_isDuringMaximizing = false;
                }
            }
            else if (!m_isDuringMaximizing)
            {
            MinWidth = 1440;
            MinHeight = 920;
            MaxWidth = 1440;
            MaxHeight = 920;
            }

            base.OnStateChanged(e);
        }

        //Generate String for Info TextBlock on initializing
        public void GenerateTakmicenjeInfoString(Takmicenje t)
        {
            String tourInfo = "";

            tourInfo += "Datum početka: " + t.GetDate().ToShortDateString() + "\n\n"
                     + "Tip takmičenja: " + t.GetTIpTakmicenja() + "\n\n" +
                       "Maksimalan broj ekipa: " + t.GetBrojUcesnika() + "\n\n";

            tbInfo.Text = tourInfo;
        }

        #endregion

        private void DodajUcesnika(object sender, RoutedEventArgs e)
        {

            lbUcesnici.ItemsSource = null;
            if (takmicenje.GetTIpTakmicenja() == "Individualno")
                DodajTakmicara();   
            else
                DodajEkipu();
            ucesnici = takmicenje.GetUcesnici();
            lbUcesnici.ItemsSource = ucesnici;

        }

        public void DodajTakmicara()
        {
            DodajTakmicara dodaj = new DodajTakmicara(ucesnici);
            if (dodaj.ShowDialog() == true)
            {
                Ucesnik ucesnik = new Takmicar(dodaj.GetNazivTakmicara(), dodaj.GetJMBG());
                if(takmicenje.DodajUcesnika(ucesnik) == false)
                {
                    MessageBox.Show("Kapacitet takmičenja je popunjen!");
                }
            }
        }

        public void DodajEkipu()
        {
            List<Ucesnik> ucesnici = takmicenje.GetUcesnici();
            DodajEkipu dodaj = new DodajEkipu(ucesnici);
            if(dodaj.ShowDialog() == true)
            {
                if(takmicenje.DodajUcesnika(dodaj.ekipa) == false)
                {
                    MessageBox.Show("Kapacitet takmičenja je popunjen!");
                }
                    
            }
        }
  

        
    }
}
