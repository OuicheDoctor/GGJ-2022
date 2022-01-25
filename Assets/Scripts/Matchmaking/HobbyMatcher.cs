using GGJ.Hobbies;
using System.Collections.Generic;
using System.Linq;

namespace GGJ.Matchmaking
{
    public class HobbyMatcher
    {
        private Dictionary<int, int> points = new Dictionary<int, int>();
        private int _nothingMatch;

        public HobbyMatcher(HobbyMatcherSettings settings)
        {
            points.Add(0, settings.zeroMatch);
            points.Add(1, settings.oneMatch);
            points.Add(2, settings.twoMatch);
            points.Add(3, settings.threeMatch);
            _nothingMatch = settings.nothingMatch;

        }

        public int Matcher(IList<HobbyData> habbiesA, IList<HobbyData> hobbiesB)
        {
            int count = 0;
            if (habbiesA.Count == 1 && habbiesA[0].name == "Nothing")
            {
                count = habbiesA.Select(h => h.category).Intersect(hobbiesB.Select(h => h.category)).Count();
                return count == 1 ? _nothingMatch : 0;
            }
            if (hobbiesB.Count == 1 && habbiesA[0].name == "Nothing")
            {
                count = habbiesA.Select(h => h.category).Intersect(hobbiesB.Select(h => h.category)).Count();
                return count == 1 ? _nothingMatch : 0;
            }
            count = habbiesA.Select(h => h.category).Intersect(hobbiesB.Select(h => h.category)).Count();
            return points[count];
        }
    }
}
