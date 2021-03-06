using GGJ.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GGJ.Matchmaking
{
    public struct Partener
    {
        public Partener(ICharacter character1, ICharacter character2, Rating rating)
        {
            Character1 = character1;
            Character2 = character2;
            Rating = rating;
        }

        public ICharacter Character1 { get; private set; }
        public ICharacter Character2 { get; private set; }

        public Rating Rating { get; private set; }

        public override string ToString() => $"{Character1.Name} x {Character2.Name} : {Rating}";
    }

    public class PartenerCollection : IEnumerable<Partener>
    {
        private Dictionary<Tuple<ICharacter, ICharacter>, Rating> _parteners;
        private Dictionary<ICharacter, int> _counters = new Dictionary<ICharacter, int>();
        private int _limit = 0;

        public List<ICharacter> Singles { get; private set; }

        public PartenerCollection(int limit)
        {
            _parteners = new Dictionary<Tuple<ICharacter, ICharacter>, Rating>();
            _limit = limit;
            Singles = new List<ICharacter>();
        }

        public bool IsPartener(ICharacter character)
        {
            return _parteners.Keys.ToList().Where(e => e.Item1 == character || e.Item2 == character).Any();
        }

        public bool IntegrityCheck()
        {
            List<ICharacter> chars = new List<ICharacter>();
            foreach (var pair in _parteners)
            {
                if (chars.Contains(pair.Key.Item1) || chars.Contains(pair.Key.Item2))
                    return false;

                chars.Add(pair.Key.Item1);
                chars.Add(pair.Key.Item2);
            }

            return !Singles.Any(s => chars.Contains(s));
        }

        public int GetEstimation()
        {
            return _parteners.Sum(p => p.Value.Scoring);
        }

        public bool RemoveByCharacter(ICharacter character)
        {
            if (IsLocked(character))
            {
                return false;
            }

            List<Tuple<ICharacter, ICharacter>> keysToRemove = _parteners.Keys.ToList().Where(e => e.Item1 == character || e.Item2 == character).ToList();

            foreach (Tuple<ICharacter, ICharacter> keyToRemove in keysToRemove)
            {
                _parteners.Remove(keyToRemove);
            }
            _counters[character] += 1;
            return true;
        }

        public Rating GetActualScore(ICharacter character)
        {
            Tuple<ICharacter, ICharacter> key = _parteners.Keys.Where(e => e.Item1 == character || e.Item2 == character).First();
            return _parteners[key];
        }

        public bool IsLocked(ICharacter character)
        {
            if (!_counters.ContainsKey(character))
            {
                _counters[character] = 0;
            }
            return _counters[character] >= _limit;
        }

        public bool ContainsKey(ICharacter character1, ICharacter character2)
        {
            Tuple<ICharacter, ICharacter> key = new Tuple<ICharacter, ICharacter>(character1, character2);
            Tuple<ICharacter, ICharacter> variant = new Tuple<ICharacter, ICharacter>(character2, character1);
            return _parteners.ContainsKey(key) || _parteners.ContainsKey(variant);
        }

        public IEnumerator<Partener> GetEnumerator()
        {
            foreach (Partener partener in _parteners.ToList().Select(i => new Partener(i.Key.Item1, i.Key.Item2, i.Value)))
            {
                yield return partener;
            }
        }

        public void Add(ICharacter character1, ICharacter character2, Rating rating)
        {
            Tuple<ICharacter, ICharacter> key = new Tuple<ICharacter, ICharacter>(character1, character2);
            _parteners.Add(key, rating);
        }

        public void AddSingle(ICharacter character)
        {
            Singles.Add(character);
        }

        public void Purify()
        {
            var tempPairs = _parteners.ToDictionary(p => p.Key, p => p.Value);

            foreach (var pair in tempPairs)
            {
                if (pair.Value.Classification < Classification.Match)
                {
                    _parteners.Remove(pair.Key);
                    AddSingle(pair.Key.Item1);
                    AddSingle(pair.Key.Item2);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}