using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GGJ.Characters;
using GGJ.Core;
using UnityEngine;


namespace GGJ.Matchmaking
{
    public class BruteForcePairMatching : Singleton<BruteForcePairMatching>
    {
        private System.Random rng = new System.Random();

        public PartenerCollection Process(IList<ICharacter> characters)
        {
            if ((characters.Count % 2) != 0)
            {
                throw new ArgumentException("The number of characters must be even.", nameof(characters));
            }

            Shuffle(characters);
            UnityEngine.Debug.Log(String.Join("; ", characters.Select(e => e.Name).ToList()));

            PartenerCollection parteners = new PartenerCollection((int)Math.Pow(characters.Count, 2));
            while (characters.Any(e => !parteners.IsPartener(e)))
            {
                foreach (ICharacter characterA in characters.Where(e => !parteners.IsPartener(e)))
                {
                    UnityEngine.Debug.Log(characterA.Name + " processing...");

                    Dictionary<ICharacter, int> scores = new Dictionary<ICharacter, int>();
                    foreach (ICharacter characterB in characters.Where(e => e != characterA))
                    {
                        int score = MatchmakingManager.Instance.Match(characterA, characterB);
                        scores.Add(characterB, score);
                    }
                    scores.OrderByDescending(e => e.Value);

                    UnityEngine.Debug.Log("Scores (" + scores.Count + ") :");

                    foreach (KeyValuePair<ICharacter, int> item in scores)
                    {
                        UnityEngine.Debug.Log(characterA.Name + " x " + item.Key.Name + " : " + item.Value);
                    }

                    foreach (KeyValuePair<ICharacter, int> item in scores)
                    {
                        Tuple<ICharacter, ICharacter> key = new Tuple<ICharacter, ICharacter>(characterA, item.Key);
                        Tuple<ICharacter, ICharacter> keyVariant = new Tuple<ICharacter, ICharacter>(item.Key, characterA);

                        if (parteners.IsPartener(characterA) || parteners.IsPartener(item.Key))
                        {
                            if (parteners.ContainsKey(key) && parteners.ContainsKey(keyVariant))
                            {
                                continue; // Already Added
                            }
                            else if (parteners.IsPartener(characterA) && !parteners.IsLocked(characterA) && parteners.GetActualScore(characterA) < item.Value)
                            {
                                parteners.RemoveCharacter(characterA);
                                parteners.Add(key, item.Value);
                                UnityEngine.Debug.Log(characterA.Name + "/" + item.Key.Name + " Added (Ref Char A)");
                            }
                            else if (parteners.IsPartener(item.Key) && !parteners.IsLocked(item.Key) && parteners.GetActualScore(item.Key) < item.Value)
                            {
                                parteners.RemoveCharacter(item.Key);
                                parteners.Add(key, item.Value);
                                UnityEngine.Debug.Log(characterA.Name + "/" + item.Key.Name + " Added (Ref Char B)");
                            }

                        }
                        else
                        {
                            parteners.Add(key, item.Value);
                            UnityEngine.Debug.Log(characterA.Name + "/" + item.Key.Name + " Added (No Ref)");
                        }
                    }
                }

            }
            return parteners;
        }

        private void Shuffle(IList<ICharacter> characters)
        {
            int n = characters.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                ICharacter value = characters[k];
                characters[k] = characters[n];
                characters[n] = value;
            }
        }

    }
}