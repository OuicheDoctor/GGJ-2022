using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GGJ.Hobbies;
using GGJ.Matchmaking;
using GGJ.Races;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GGJ.Characters
{
    [Serializable]
    public class Character : ICharacter
    {
        #region Exposed API

        public string Name { get; set; }
        public RaceData Race { get; set; }
        public Sprite Drawing { get; set; }
        public bool TraitEI { get; set; }
        public bool TraitSN { get; set; }
        public bool TraitTF { get; set; }
        public bool TraitJP { get; set; }
        public IList<HobbyData> Hobbies { get; private set; }
        public string Region { get; set; }

        public Character()
        {
            Hobbies = new List<HobbyData>();
        }

        public void ChangeRace(RaceData newRace, List<string> usedNames)
        {
            Race = newRace;
            Name = newRace.Names.Where(n => !usedNames.Contains(n)).PickOne();
        }

        public void ChangeRegion(string newRegion)
        {
            Region = newRegion;
        }

        public void ForgeMatchingFor(Character mate, Classification classification)
        {
            switch (classification)
            {
                case Classification.Perfect:
                    {
                        MatchTraits(mate, 3);
                        // Special case for hobby "Nothing"
                        MatchHobbies(mate, Mathf.Min(mate.Hobbies.Count, Random.Range(1, 3)));
                        break;
                    }
                case Classification.Kill:
                    {
                        int nbSharedTrait = new List<int> { 0, 4 }.PickOne();
                        if (nbSharedTrait == 0)
                        {
                            int nbHobby = mate.Hobbies.Any(m => m.name == "Nothing") ? 0 : new List<int> { 0, 3 }.PickOne();
                            if (nbHobby == 0)
                            {
                                // 0 trait, 0 hobby
                                MatchTraits(mate, 0);
                                MatchHobbies(mate, 0);
                            }
                            else
                            {
                                // 0 trait, 3 hobbies
                                MatchTraits(mate, 0);
                                MatchHobbies(mate, 3);
                            }
                        }
                        else
                        {
                            // 4 traits, 0 hobby
                            MatchTraits(mate, 4);
                            MatchHobbies(mate, 0);
                        }
                        break;
                    }
            }
        }

        public bool GetMBTITrait(MBTITrait axis)
        {
            switch (axis)
            {
                case MBTITrait.ExtravertiIntraverti:
                    return TraitEI;

                case MBTITrait.SensationIntuition:
                    return TraitSN;

                case MBTITrait.PenseeSentiments:
                    return TraitTF;

                case MBTITrait.JugementPerception:
                    return TraitTF;

                default:
                    throw new Exception("Unkown trait");
            }
        }

        public void SetMBTITrait(MBTITrait axis, bool value)
        {
            switch (axis)
            {
                case MBTITrait.ExtravertiIntraverti:
                    TraitEI = value;
                    break;

                case MBTITrait.SensationIntuition:
                    TraitSN = value;
                    break;

                case MBTITrait.PenseeSentiments:
                    TraitTF = value;
                    break;

                case MBTITrait.JugementPerception:
                    TraitTF = value;
                    break;

                default:
                    throw new Exception("Unkown trait");
            }
        }

        #endregion Exposed API

        private void MatchTraits(Character mate, int countToMatch)
        {
            var traitList = Enum.GetValues(typeof(MBTITrait)).Cast<MBTITrait>().ToList();
            MBTITrait current;
            for (var i = 0; i < countToMatch; i++)
            {
                current = traitList.PickOneAndRemove();
                SetMBTITrait(current, mate.GetMBTITrait(current));
            }

            foreach (var remaining in traitList)
            {
                SetMBTITrait(remaining, !mate.GetMBTITrait(remaining));
            }
        }

        private void MatchHobbies(Character mate, int countToMatch)
        {
            Hobbies.Clear();
            var hobbies = GameManager.Instance.Settings.Hobbies;
            var hobbyNothing = hobbies.FirstOrDefault(h => h.name == "Nothing");
            var categories = mate.Hobbies.Select(h => h.category).ToList();
            CategoryData currentCategory;
            HobbyData currentHobby;
            for (var i = 0; i < countToMatch; i++)
            {
                currentCategory = categories.PickOne();
                currentHobby = hobbies.Where(h => h.category == currentCategory).PickOne();
                if (currentHobby == hobbyNothing)
                {
                    Hobbies.Clear();
                    Hobbies.Add(currentHobby);
                    return;
                }
                else
                {
                    Hobbies.Add(currentHobby);
                }
            }

            List<HobbyData> otherHobbies = hobbies.Where(h => !categories.Contains(h.category) && h != hobbyNothing).ToList();
            for (var i = 0; i < mate.Hobbies.Count - countToMatch; i++)
            {
                currentHobby = otherHobbies.PickOne();
                Hobbies.Add(currentHobby);
                otherHobbies.RemoveAll(h => h.category == currentHobby.category);
            }
        }
    }
}