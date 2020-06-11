namespace PixelCurio.AlteredTimeline
{
    public class Defend : IAction
    {
        public string Name { get; } = "Defend";
        public int ManaCost { get; } = 0;
        public void ApplyEffect(ICharacter target) => target.ReceiveDamage(-10);
    }
}
