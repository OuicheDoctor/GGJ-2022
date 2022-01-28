using GGJ.Characters;
using GGJ.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GGJ.Matchmaking
{
    public class BruteForcePairMatching : Singleton<BruteForcePairMatching>
    {
        private System.Random _random = new System.Random();

        public PartenerCollection Process(IList<ICharacter> characters)
        {
            if ((characters.Count % 2) != 0)
            {
                throw new ArgumentException("The number of characters must be even.", nameof(characters));
            }

            Shuffle(characters);
            PartenerCollection parteners = new PartenerCollection(characters.Count);
            bool everybodyIsPaired = false;
            while (!everybodyIsPaired)
            {
                foreach (ICharacter characterA in characters.Where(e => !parteners.IsPartener(e)))
                {
                    if (parteners.IsLocked(characterA))
                    {
                        continue;
                    }

                    var scores = GetScores(characterA, characters);
                    foreach (KeyValuePair<ICharacter, int> item in scores)
                    {
                        if (parteners.IsLocked(characterA))
                        {
                            break;
                        }
                        if (parteners.ContainsKey(characterA, item.Key) || parteners.IsLocked(item.Key))
                        {
                            continue;
                        }

                        if (parteners.IsPartener(characterA) && parteners.GetActualScore(characterA) < item.Value)
                        {
                            parteners.RemoveByCharacter(characterA);
                            parteners.Add(characterA, item.Key, item.Value);
                        }
                        else if (parteners.IsPartener(item.Key) && parteners.GetActualScore(item.Key) < item.Value)
                        {
                            parteners.RemoveByCharacter(item.Key);
                            parteners.Add(characterA, item.Key, item.Value);
                        }
                        else if (!parteners.IsPartener(characterA) && !parteners.IsPartener(item.Key))
                        {
                            parteners.Add(characterA, item.Key, item.Value);
                        }
                    }

                    if (!parteners.IsPartener(characterA))
                        parteners.AddSingle(characterA); // Handle singles
                }
                everybodyIsPaired = characters.All(e => parteners.IsPartener(e) || parteners.Singles.Contains(e));
            }
            return parteners;
        }

        private IOrderedEnumerable<KeyValuePair<ICharacter, int>> GetScores(ICharacter characterReference, IList<ICharacter> characters)
        {
            Dictionary<ICharacter, int> scores = new Dictionary<ICharacter, int>();
            foreach (ICharacter characterTargeted in characters.Where(e => e != characterReference))
            {
                int score = MatchmakingManager.Instance.Match(characterReference, characterTargeted);
                scores.Add(characterTargeted, score);
            }
            return scores.OrderByDescending(e => e.Value);
        }

        private void Shuffle(IList<ICharacter> characters)
        {
            int n = characters.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                ICharacter value = characters[k];
                characters[k] = characters[n];
                characters[n] = value;
            }
        }
    }
}