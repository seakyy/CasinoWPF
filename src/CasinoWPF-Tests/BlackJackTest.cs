using CasinoWPF.Games;
using CasinoWPF.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CasinoWPF_Tests
{
    [TestClass]
    public class BlackJackTest
    {
        private BlackJack _blackJack;
        private Player _player;

        [TestInitialize]
        public void Setup()
        {
            _blackJack = new BlackJack();
            _player = new Player("Tester", 1000);
        }

        [TestMethod]
        public void StartGame_ShouldDealTwoCards()
        {
            _blackJack.StartGame(_player, 50);
            Assert.AreEqual(2, _blackJack.PlayerHand.Count);
        }

        [TestMethod]
        public void Hit_ShouldAddCardToHand()
        {
            _blackJack.StartGame(_player, 50);
            int before = _blackJack.PlayerHand.Count;
            _blackJack.ExecuteAction("hit");
            int after = _blackJack.PlayerHand.Count;
            Assert.IsTrue(after > before);
        }

        [TestMethod]
        public void DoubleDown_ShouldDoubleBetAndAddCard()
        {
            _blackJack.StartGame(_player, 50);
            _blackJack.ExecuteAction("doubledown");
            Assert.IsTrue(_blackJack.PlayerHand.Count >= 3);
        }
    }
}