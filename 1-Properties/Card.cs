namespace Properties
{
    using System;

    /// <summary>
    /// The class models a card.
    /// </summary>
    public class Card
    {
        string Seed { get; }

        string Name { get; }

        int Ordinal { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        /// <param name="name">the name of the card.</param>
        /// <param name="seed">the seed of the card.</param>
        /// <param name="ordinal">the ordinal number of the card.</param>
        public Card(string name, string seed, int ordinal)
        {
            this.Name = name;
            this.Ordinal = ordinal;
            this.Seed = seed;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        /// <param name="tuple">the informations about the card as a tuple.</param>
        internal Card(Tuple<string, string, int> tuple)
            : this(tuple.Item1, tuple.Item2, tuple.Item3)
        {
        }

        public string GetSeed()
        {
            return this.Seed;
        }

       public string GetName()
        {
            return this.Name;
        }

        public int GetOrdinal()
        {
            return this.Ordinal;
        }

        /// <inheritdoc cref="object.ToString"/>
        public override string ToString()
        {
            // TODO understand string interpolation
            return $"{this.GetType().Name}(Name={this.GetName()}, Seed={this.GetSeed()}, Ordinal={this.GetOrdinal()})";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Card))
                return false;
            Card other = (Card) obj;
            return this.Ordinal == other.Ordinal && this.Seed.Equals(other.GetSeed());
        }

        public override int GetHashCode()
        {
            return this.Ordinal.GetHashCode() + this.Seed.GetHashCode();
        }
    }
}
