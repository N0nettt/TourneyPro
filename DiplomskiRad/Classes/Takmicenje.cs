using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DiplomskiRad.Classes
{
    public class Takmicenje
    {
        private string nazivTakmicenja;
        private DateTime date;
        private int brojUcesnika;
        private string tipTakmicenja;
        private ObservableCollection<Ucesnik> ucesnici;
        private Ucesnik? pobednik;
        public Zreb zreb;
        

        public Takmicenje(string nazivTakmicenja, DateTime date, int brojUcesnika, string tipTakmicenja)
        {
            this.nazivTakmicenja = nazivTakmicenja;
            this.date = date;
            this.brojUcesnika = brojUcesnika;
            this.tipTakmicenja = tipTakmicenja;
            this.zreb = new Zreb(this);
            ucesnici = new ObservableCollection<Ucesnik>();        
        }

        public bool KonstruisiZreb()
        {
            if(ucesnici.Count() == brojUcesnika)
            {
                this.zreb.KonstruisiZreb();
                return true;
            }
            return false;
        }

        #region GettersAndSetters
        public void ProglasiPobednika(Ucesnik pobednik)
        {
            MessageBox.Show("Pobednik takmicenja je: " + pobednik.GetNazivUcesnika(), "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            this.pobednik = pobednik;
        }
        public string GetPobednik()
        {
            if (pobednik != null)
                return pobednik.GetNazivUcesnika();
            else
                return "Pobednik nije proglasen";
        }

        public string GetNazivTakmicenja()
        {
            return nazivTakmicenja;
        }
        public void SetNazivTakmicenja(string naziv)
        {
            this.nazivTakmicenja = naziv;
        }

        public DateTime GetDate()
        {
            return date;
        }
        public void SetDate(DateTime date)
        {
            this.date = date;
        }

        public int GetBrojUcesnika()
        {
            return brojUcesnika;
        }
        public void SetBrojUcesnika(int brojUcesnika)
        {
            this.brojUcesnika = brojUcesnika;
        }
        public string GetTIpTakmicenja()
        {
            return tipTakmicenja;
        }
        public void SetTipTakmicenja(string tipTakmicenja)
        {
            this.tipTakmicenja = tipTakmicenja;
        }
        public ObservableCollection<Ucesnik> GetUcesnici()
        {
            return ucesnici;
        }

        public bool DodajUcesnika(Ucesnik ucesnik)
        {
            if(ucesnici.Count() < brojUcesnika)
            {
                ucesnici.Add(ucesnik);
                return true;
            }
            return false;
        } 

        public void SetUcesnici(ObservableCollection<Ucesnik> u)
        {
            this.ucesnici = u;
        }

        public void RemoveParticipant(Ucesnik u)
        {
            ucesnici.Remove(u);
        }

        public int GetNumOfRegisteredParticipants()
        {
            return ucesnici.Count;
        }

        public void ResetBracket()
        {
            zreb.ResetBracket();
        }

        public void SetSpecificParticipant(int index, Ucesnik u)
        {
            ucesnici[index] = u;
        }
        #endregion







    }
}
