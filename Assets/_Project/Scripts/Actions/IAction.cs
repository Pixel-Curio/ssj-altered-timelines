namespace PixelCurio.AlteredTimeline
{
    public interface IAction
    {
        string Name { get; }
        int ManaCost { get; }
        void ApplyDamage(ICharacter target);
    }
}
