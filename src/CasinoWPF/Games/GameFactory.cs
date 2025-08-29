using System;
using System.Collections.Generic;
using CasinoWPF.Models;


namespace CasinoWPF.Games
{
    // Factory pattern
    public class GameFactory
    {
        private static readonly Lazy<GameFactory> _instance = new Lazy<GameFactory>(() => new GameFactory());

        public static GameFactory Instance => _instance.Value;

        private readonly Dictionary<GameType, Func<IGame>> _gameCreators;

        private GameFactory()
        {
            _gameCreators = new Dictionary<GameType, Func<IGame>>
            {
                { GameType.BlackJack, () => new BlackJack() },
                { GameType.SlotMachine, () => new SlotMachine() }
            };
        }

        public IGame CreateGame(GameType gameType)
        {
            if (_gameCreators.TryGetValue(gameType, out var creator))
            {
                return creator();
            }

            throw new ArgumentException($"Spieltyp {gameType} wird nicht unterstützt.");
        }

        public IEnumerable<IGame> CreateAllGames()
        {
            var games = new List<IGame>();

            foreach (var creator in _gameCreators.Values)
            {
                games.Add(creator());
            }

            return games;
        }
    }
}