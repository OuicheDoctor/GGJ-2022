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
    public struct Rating
    {
        [SerializeField] private int _low;
        [SerializeField] private int _hight;
        [SerializeField] private string _result;
        [SerializeField] private Sprite _resultIcon;
        [SerializeField] private int _scoring;
        [SerializeField] private Classification _classificationName;
        [SerializeField] private LoveStatus _status;

        public int Low => _low;
        public int Hight => _hight;
        public string Result => _result;
        public Sprite ResultIcon => _resultIcon;
        public int Scoring => _scoring;
        public LoveStatus Status => _status;
        public Classification ClassificationName => _classificationName;
    }

    public enum Classification
    {
        Single = -1,
        Kill = 0,
        NoMatch = 1,
        Match = 2,
        Perfect = 3,
    }

    [CreateAssetMenu(fileName = "MatchmakingSettings", menuName = "GGJ/Matchmaking Settings")]
    public class MatchmakingSettings : ScriptableObject
    {
        [SerializeField] private List<Counter> _traitScoring;
        [SerializeField] private List<Counter> _hobbyScoring;
        [SerializeField] private int _nothingHobbyScoring;
        [SerializeField] private List<Rating> _classification;
        [SerializeField] private Rating _singleClassification;

        public List<Counter> TraitScoring => _traitScoring;
        public List<Counter> HobbyScoring => _hobbyScoring;
        public int NothingHobbyScoring => _nothingHobbyScoring;

        public List<Rating> Classification => _classification;
        public Rating SingleClassification => _singleClassification;

        public Rating BestClassification => _classification.FirstOrDefault(c => c.ClassificationName == Matchmaking.Classification.Perfect);

        public Rating GetMatchingClassification(int value)
        {
            return _classification.Where(r => r.Low <= value && r.Hight >= value).FirstOrDefault();
        }
    }
}