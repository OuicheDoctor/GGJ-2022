using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using GGJ.Characters;
using GGJ.Hobbies;
using UnityEngine;

namespace GGJ.Matchmaking
{
    public class TraitMatcher
    {
        private Dictionary<int, int> points = new Dictionary<int, int>();

        public TraitMatcher(TraitMatcherSettings settings)
        {
            points.Add(0, settings.zeroMatch);
            points.Add(1, settings.oneMatch);
            points.Add(2, settings.twoMatch);
            points.Add(3, settings.threeMatch);
            points.Add(4, settings.fourMatch);
        }

        public int Matcher(ICharacter characterA, ICharacter characterB)
        {
            var count = 0;

            if (characterA.TraitEI == characterB.TraitEI)
            {
                count += 1;
            }
            if (characterA.TraitJP == characterB.TraitJP)
            {
                count += 1;
            }
            if (characterA.TraitSN == characterB.TraitSN)
            {
                count += 1;
            }
            if (characterA.TraitTF == characterB.TraitTF)
            {
                count += 1;
            }
            return points[count];
        }
    }
}