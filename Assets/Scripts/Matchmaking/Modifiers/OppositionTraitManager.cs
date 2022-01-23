
using System.Collections.Generic;
using System.Linq;
using GGJ.Character;

namespace GGJ.Matchmaking.Modifiers
{
    public class OppositionTraitManager : TraitModifierBase
    {

        private OppositionModifierData oppositionModifier;
        public OppositionTraitManager(OppositionModifierData oppositionModifier)
        {
            oppositionModifier = oppositionModifier;
        }

        public string Key
        {
            get { return oppositionModifier.TriggerTrait; }
        }
        public int GetScoreModifier(List<ITrait> traits)
        {
            if (traits.Any(e => e.Name.ToLower() == oppositionModifier.TargetTrait.ToLower()))
            {
                return oppositionModifier.Modifier;
            }
            return 0;
        }
    }
}
