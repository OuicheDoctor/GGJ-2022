namespace GGJ.Character
{
    public interface ITrait
    {
        public bool Value { get; set; }
        public ITraitCategory TraitCategory { get; set; }
    }
}