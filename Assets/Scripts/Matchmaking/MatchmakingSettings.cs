using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GGJ.Matchmaking
{
    [Serializable]
    public struct Counter
    {
        [SerializeField] private int _count;
        [SerializeField] private int _percent;

        public int Count => _count;
        public int Percent => _percent;
    }

    [Serializable]
    public struct Range
    {
        [SerializeField] private int _low;
        [SerializeField] private int _hight;
        [SerializeField] private string _result;
        [SerializeField] private Sprite _resultIcon;

        public int Low => _low;
        public int Hight => _hight;
        public string Result => _result;
        public Sprite ResultIcon => _resultIcon;
    }

    [CreateAssetMenu(fileName = "MatchmakingSettings", menuName = "GGJ/Matchmaking Settings")]
    public class MatchmakingSettings : ScriptableObject
    {
        [SerializeField] private List<Counter> _traitScoring;
        [SerializeField] private List<Counter> _hobbyScoring;
        [SerializeField] private int _nothingHobbyScoring;
        [SerializeField] private List<Range> _classification;

        public List<Counter> TraitScoring => _traitScoring;
        public List<Counter> HobbyScoring => _hobbyScoring;
        public int NothingHobbyScoring => _nothingHobbyScoring;

        public List<Range> Classification => _classification;

        public Range GetMatchingClassification(int value)
        {
            return _classification.Where(r => r.Low <= value && r.Hight >= value).FirstOrDefault();
        }
    }
}