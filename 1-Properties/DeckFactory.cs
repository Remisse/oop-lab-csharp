namespace Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A factory class for building <see cref="ISet{T}">decks</see> of <see cref="Card"/>s.
    /// </summary>
    public class DeckFactory
    {
        private string[] seeds;
        private string[] names;

        IList<string> Seeds
        {
            get => this.seeds.ToList();
            set => this.seeds = value.ToArray();
        }

        IList<string> Names
        {
            get => this.names.ToList();
            set => this.names = value.ToArray();
        }

        int DeckSize => this.seeds.Length * this.names.Length;

        public IList<string> GetSeeds()
        {
            return this.Seeds;
        }

        public void SetSeeds(IList<string> seeds)
        {
            this.Seeds = seeds;
        }

        public IList<string> GetNames()
        {
            return this.Names;
        }

        public void SetNames(IList<string> names)
        {
            this.Names = names;
        }

        public int GetDeckSize()
        {
            return this.DeckSize;
        }

        /// TODO improve
        public ISet<Card> GetDeck()
        {
            if (this.names == null || this.seeds == null)
            {
                throw new InvalidOperationException();
            }

            return new HashSet<Card>(Enumerable
                .Range(0, this.names.Length)
                .SelectMany(i => Enumerable
                    .Repeat(i, this.seeds.Length)
                    .Zip(
                        Enumerable.Range(0, this.seeds.Length),
                        (n, s) => Tuple.Create(this.names[n], this.seeds[s], n)))
                .Select(tuple => new Card(tuple))
                .ToList());
        }
    }
}
