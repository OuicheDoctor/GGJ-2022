using UnityEngine;

namespace GGJ.Matchmaking.Modifiers
{
    [CreateAssetMenu(fileName = "OppositionModifierData", menuName = "GGJ/Modifiers/Opposition Data")]
    public class OppositionModifierData : ScriptableObject
    {

        public int Modifier = 0;
        public string TriggerTrait = "";

        public string TargetTrait = "";

        public string Description = "";

    }
}