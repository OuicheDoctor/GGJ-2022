using GGJ.Characters;
using System.Collections.Generic;
using System.Linq;

namespace GGJ.Matchmaking
{
    public class TraitMatcher
    {
        private Dictionary<int, int> _percents;

        public TraitMatcher(List<Counter> scoreDefinitions)
        {
            _percents = scoreDefinitions.ToDictionary(d => d.Count, d => d.Percent);
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
            return _percents[count];
        }
    }
}