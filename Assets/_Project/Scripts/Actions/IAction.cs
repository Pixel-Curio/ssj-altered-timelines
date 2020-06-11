namespace PixelCurio.AlteredTimeline
{
    public interface IAction
    {
        string Name { get; }
        int ManaCost { get; }
        void ApplyEffect(ICharacter target);
    }
}
