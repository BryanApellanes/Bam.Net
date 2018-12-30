using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Testing;
using Bam.Net;

namespace Bam.Net.Cards.Tests
{
    [Serializable]
    public class UnitTests: CommandLineTestInterface
    {
        [UnitTest]
        public void FullDeckShouldHave52Cards()
        {
            Deck deck = new Deck();
            Expect.IsNotNull(deck.Cards, "Cards was null");
            Expect.AreEqual(52, deck.Cards.Count(), $"Expected 52 cards but there were {deck.Cards.Count()}");
            ShowCards(deck);
        }
        [UnitTest]
        public void ShouldBeFullDeckAfterShuffle()
        {
            Deck deck = new Deck();
            deck.Shuffle();
            Expect.AreEqual(52, deck.Cards.Count(), $"Expected 52 cards but there were {deck.Cards.Count()}");
            ShowCards(deck);
        }

        private void ShowCards(Deck deck)
        {
            foreach(Card card in deck.Cards)
            {
                OutLineFormat($"{card.Value} of {card.Suit}", ConsoleColor.Cyan);
            }
        }
    }
}
