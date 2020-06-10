namespace PixelCurio.AlteredTimeline
{
    public class Attack : IAction
    {
        public string Name { get; } = "Attack";
        public int ManaCost { get; } = 0;
        public void ApplyDamage(ICharacter target) => target.ReceiveDamage(10);
    }
}
