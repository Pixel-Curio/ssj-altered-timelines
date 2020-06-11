namespace PixelCurio.AlteredTimeline
{
    public class Attack : IAction
    {
        public string Name { get; } = "Attack";
        public int ManaCost { get; } = 0;
        public void ApplyEffect(ICharacter target) => target.ReceiveDamage(10);
    }
}
