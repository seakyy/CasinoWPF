using CasinoWPF.Games;
using CasinoWPF.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CasinoWPF_Tests
{
    [TestClass]
    public class SlotMachineTest
    {
        private SlotMachine _slotMachine;
        private Player _player;

        [TestInitialize]
        public void Setup()
        {
            _slotMachine = new SlotMachine();
            _player = new Player("TestPlayer", 1000);
        }

        [TestMethod]
        public void StartGame_ValidBet_ReturnsTrue()
        {
            bool started = _slotMachine.StartGame(_player, 50);
            Assert.IsTrue(started);
            Assert.AreEqual(950, _player.Balance);
        }

        [TestMethod]
        public void StartGame_InvalidBet_ReturnsFalse()
        {
            bool started = _slotMachine.StartGame(_player, 10000);
            Assert.IsFalse(started);
        }

        [TestMethod]
        public void ExecuteAction_Spin_ChangesReels()
        {
            _slotMachine.StartGame(_player, 50);
            var before = _slotMachine.CurrentReels.ToArray();
            _slotMachine.ExecuteAction("spin");
            var after = _slotMachine.CurrentReels.ToArray();

            CollectionAssert.AreNotEqual(before, after);
        }

        [TestMethod]
        public void EndGame_WithoutStart_ReturnsNull()
        {
            var result = _slotMachine.EndGame();
            Assert.IsNull(result);
        }
    }
}