using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using GGJ.Characters;
using UnityEngine;

namespace GGJ.Matchmaking
{
    public class PartenerCollection : Dictionary<Tuple<ICharacter, ICharacter>, int>
    {
        private Dictionary<ICharacter, int> _counters = new Dictionary<ICharacter, int>();
        private int _limit = 0;

        public PartenerCollection(int limit)
        {
            _limit = limit;
        }
        public bool IsPartener(ICharacter character)
        {
            return Keys.ToList().Where(e => e.Item1 == character || e.Item2 == character).Any();
        }

        public bool RemoveCharacter(ICharacter character)
        {
            if (IsLocked(character))
            {
                UnityEngine.Debug.Log(character.Name + " is Locked");
                return false;
            }
            List<Tuple<ICharacter, ICharacter>> keysToRemove = Keys.ToList().Where(e => e.Item1 == character || e.Item2 == character).ToList();
            foreach (Tuple<ICharacter, ICharacter> keyToRemove in keysToRemove)
            {
                Remove(keyToRemove);
            }
            _counters[character] += 1;
            return true;
        }

        public int GetActualScore(ICharacter character)
        {
            Tuple<ICharacter, ICharacter> key = Keys.Where(e => e.Item1 == character || e.Item2 == character).First();
            return this[key];
        }

        public bool IsLocked(ICharacter character)
        {
            if (!_counters.ContainsKey(character))
            {
                _counters[character] = 0;
            }
            return _counters[character] >= _limit;
        }
    }
}