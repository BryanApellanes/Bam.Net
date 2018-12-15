using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;

namespace Bam.Net.Cards
{
    public class Deck
    {
        public Deck() : this(GetCards()) { }
        public Deck(IEnumerable<Card> cards)
        {
            Cards = cards;
        }

        public IEnumerable<Card> Cards { get; set; }
        public void Shuffle()
        {
            List<Card> shuffled = new List<Card>();
            List<Card> current = new List<Card>(Cards);
            int count = current.Count;
            for(int i = 0; i < count; i++)
            {
                int randomPosition = RandomNumber.Between(0, current.Count);
                shuffled.Add(current[randomPosition]);
                current.RemoveAt(randomPosition);
            }
            Cards = shuffled;
        }

        private static List<Card> GetCards()
        {
            List<Card> cards = new List<Card>();
            for (int suit = 0; suit <= (int)CardSuits.Hearts; suit++)
            {
                for (int value = 2; value <= (int)CardValues.Ace; value++)
                {
                    cards.Add(new Card { Suit = (CardSuits)suit, Value = (CardValues)value });
                }
            }

            return cards;
        }
    }
}
