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
    /// Interaction logic for EditParticipant.xaml
    /// </summary>
    public partial class EditParticipant : Window
    {
        public Takmicar participant { get; set; }

        public EditParticipant(Takmicar u)
        {
            InitializeComponent();
            tbJMBG.Text = u.GetJmbg();
            tbNazivTakmicara.Text = u.GetNazivUcesnika();
            participant = u;
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            if(MessageBoxResult.Yes == MessageBox.Show("Are you done?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                participant.SetNazivUcesnika(tbNazivTakmicara.Text);
                participant.SetJmbg(tbJMBG.Text);
                this.DialogResult = true;
                this.Close(); 
            }
        }
    }
}
