using CasinoWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoWPF.Services
{

    // Session-Daten, die von Beobachtern empfangen werden
    public class SessionData
    {
        public Player CurrentPlayer { get; set; }
        public GameType? CurrentGameType { get; set; }
        public IGame CurrentGame { get; set; }
        public int LastBet { get; set; }
        public GameResult LastResult { get; set; }
    }

    // Singleton Session-Klasse, die das Observable Pattern implementiert
    public class Session : Observable<SessionData>
    {
        private static readonly Lazy<Session> _instance = new Lazy<Session>(() => new Session());

        public static Session Instance => _instance.Value;

        private Session()
        {
            // Initialisiere den State
            State = new SessionData();
        }

        public Player CurrentPlayer
        {
            get => State.CurrentPlayer;
            set
            {
                State.CurrentPlayer = value;
                NotifyObservers();
            }
        }

        public GameType? CurrentGameType
        {
            get => State.CurrentGameType;
            set
            {
                State.CurrentGameType = value;
                NotifyObservers();
            }
        }

        public IGame CurrentGame
        {
            get => State.CurrentGame;
            set
            {
                State.CurrentGame = value;
                NotifyObservers();
            }
        }

        public int LastBet
        {
            get => State.LastBet;
            set
            {
                State.LastBet = value;
                NotifyObservers();
            }
        }

        public GameResult LastResult
        {
            get => State.LastResult;
            set
            {
                State.LastResult = value;
                NotifyObservers();
            }
        }

        public void StartNewSession(string playerName, int initialBalance)
        {
            CurrentPlayer = new Player(playerName, initialBalance);
            CurrentGameType = null;
            CurrentGame = null;
            LastBet = 0;
            LastResult = null;

            NotifyObservers();
        }

        public void EndSession()
        {
            CurrentPlayer = null;
            CurrentGameType = null;
            CurrentGame = null;
            LastBet = 0;
            LastResult = null;

            NotifyObservers();
        }
    }
}
