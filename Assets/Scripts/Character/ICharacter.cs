using System.Collections.Generic;

namespace GGJ.Character
{
    public interface ICharacter
    {
        public List<ITrait> GetTraits();
    }
}