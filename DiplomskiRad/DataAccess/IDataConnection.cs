using DiplomskiRad.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiRad.Database
{
    public interface IDataConnection
    {
        Participant CreateParticipant(Participant participant);
        Tournament CreateTournament(Tournament Tournament);
        int CreateBracket(Bracket bracket);
        void CreateMatch(Match match);
        Prize CreatePrize(Prize prize);
        Round CreateRound(Round round);
        User CreateUser(User user);
        int SelectBracketID(Tournament tournament);
        Role CreateRole(Role role);
        User LoginUser(String username, String password);
        ObservableCollection<Tournament> SelectParticipantTournaments(User user);
        void SetWinner(Participant p, Tournament t);
        void CreateTournamentParticipant(Participant p, Tournament t);
        void DeleteTournamentParticipant(Participant p, Tournament t);
        void DeleteMatches(Round r, Tournament t);
        void DeleteRounds(Bracket b);
        void UpdateParticipants(Match m);
        void UpdateTheWinner(Match m, Participant p);
        ObservableCollection<Tournament> SelectTournamentsByOrganizer(User user);
        void DeletePrizes(Tournament t);
        ObservableCollection<Participant> SelectParticipants();
        void UpdateParticipant(Participant p);
        //Fully load the selected Tournament
        Tournament LoadTournament(Tournament t);
        //Delete Tournament from Database
        void DeleteTournament(Tournament t);
        ObservableCollection<User> SelectUsers();
        void UpdateUser(User user);
        void DeleteUser(User user);
        ObservableCollection<Role> SelectRoles();

    }
}
