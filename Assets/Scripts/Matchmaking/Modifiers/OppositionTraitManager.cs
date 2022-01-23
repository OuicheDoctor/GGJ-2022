
using System.Collections.Generic;
using System.Linq;
using GGJ.Character;

namespace GGJ.Matchmaking.Modifiers
{
    public class OppositionTraitManager
    {

        private OppositionModifierData _oppositionModifier;

        public OppositionTraitManager(OppositionModifierData oppositionModifier)
        {
            _oppositionModifier = oppositionModifier;
        }

        public string Key
        {
            get { return _oppositionModifier.TriggerTrait; }
        }
        
        public int GetScoreModifier(List<ITrait> traits)
        {
            if (traits.Any(e => e.Name.ToLower() == _oppositionModifier.TargetTrait.ToLower()))
            {
                return _oppositionModifier.Modifier;
            }
            return 0;
        }
    }
}
