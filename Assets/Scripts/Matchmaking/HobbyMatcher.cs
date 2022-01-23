using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using GGJ.Hobbies;
using UnityEngine;

namespace GGJ.Matchmaking
{
    public class HobbyMatcher
    {
        private Dictionary<int, int> points = new Dictionary<int, int>();

        public HobbyMatcher(HobbyMatcherData settings){
            points.Add(0, settings.zeroMatch);
            points.Add(1, settings.oneMatch);
            points.Add(2, settings.twoMatch);
            points.Add(3, settings.threeMatch);
        }

        public int Matcher(IList<HobbyData> habbiesA, IList<HobbyData> hobbiesB) {    
            var count = habbiesA.Select(h => h.category).Intersect(hobbiesB.Select(h => h.category)).Count();

            return points[count];
        }
    }
}
