using System.Collections.Immutable;

namespace Indexers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <inheritdoc cref="IMap2D{TKey1,TKey2,TValue}" />
    public class Map2D<TKey1, TKey2, TValue> : IMap2D<TKey1, TKey2, TValue>
    {
        readonly IDictionary<Tuple<TKey1, TKey2>, TValue> map;

        public Map2D()
        {
            this.map = new Dictionary<Tuple<TKey1, TKey2>, TValue>();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.NumberOfElements" />
        public int NumberOfElements => map.Values.Count;

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.this" />
        public TValue this[TKey1 key1, TKey2 key2]
        {
            get
            {
                map.TryGetValue(new Tuple<TKey1, TKey2>(key1, key2), out TValue output);
                return output;
            }
            set => map.Add(new Tuple<TKey1, TKey2>(key1, key2), value);
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetRow(TKey1)" />
        public IList<Tuple<TKey2, TValue>> GetRow(TKey1 key1)
        {
            return this.map
                .Where(e => e.Key.Item1.Equals(key1))
                .Select(e => new Tuple<TKey2, TValue>(e.Key.Item2, e.Value))
                .ToImmutableList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetColumn(TKey2)" />
        public IList<Tuple<TKey1, TValue>> GetColumn(TKey2 key2)
        {
            return this.map
                .Where(e => e.Key.Item1.Equals(key2))
                .Select(e => new Tuple<TKey1, TValue>(e.Key.Item1, e.Value))
                .ToImmutableList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetElements" />
        public IList<Tuple<TKey1, TKey2, TValue>> GetElements()
        {
            return this.map
                .Select(e => new Tuple<TKey1, TKey2, TValue>
                    (e.Key.Item1, e.Key.Item2, e.Value))
                .ToImmutableList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.Fill(IEnumerable{TKey1}, IEnumerable{TKey2}, Func{TKey1, TKey2, TValue})" />
        public void Fill(IEnumerable<TKey1> keys1, IEnumerable<TKey2> keys2, Func<TKey1, TKey2, TValue> generator)
        {
            foreach (var i in keys1)
            {
                foreach (var j in keys2)
                {
                    this.map.Add(new Tuple<TKey1, TKey2>(i, j), generator.Invoke(i, j));
                }
            }
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)" />
        public bool Equals(IMap2D<TKey1, TKey2, TValue> other)
        {
            return other != null && this.GetElements().Equals(other.GetElements());
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            var other = obj as Map2D<TKey1, TKey2, TValue>;
            return this.Equals(other);
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            return this.map.GetHashCode();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.ToString"/>
        public override string ToString()
        {
            return this.map
                .Select(e => $"[{e.Key.Item1},{e.Key.Item2}] -> {e.Value}")
                .Aggregate((s1, s2) => s1 + "\n" + s2);
        }
    }
}
