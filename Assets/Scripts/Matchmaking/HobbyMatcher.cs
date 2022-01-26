using GGJ.Hobbies;
using System.Collections.Generic;
using System.Linq;

namespace GGJ.Matchmaking
{
    public class HobbyMatcher
    {
        private Dictionary<int, int> _percents;
        private int _nothingPercents;

        public HobbyMatcher(List<Counter> scoreDefinitions, int nothingPercents)
        {
            _percents = scoreDefinitions.ToDictionary(d => d.Count, d => d.Percent);
            _nothingPercents = nothingPercents;
        }

        public int Matcher(IList<HobbyData> habbiesA, IList<HobbyData> hobbiesB)
        {
            int count = 0;
            if (habbiesA.Count == 1 && habbiesA[0].name == "Nothing")
            {
                count = habbiesA.Select(h => h.category).Intersect(hobbiesB.Select(h => h.category)).Count();
                return count == 1 ? _nothingPercents : 0;
            }
            if (hobbiesB.Count == 1 && habbiesA[0].name == "Nothing")
            {
                count = habbiesA.Select(h => h.category).Intersect(hobbiesB.Select(h => h.category)).Count();
                return count == 1 ? _nothingPercents : 0;
            }
            count = habbiesA.Select(h => h.category).Intersect(hobbiesB.Select(h => h.category)).Count();
            return _percents[count];
        }
    }
}
