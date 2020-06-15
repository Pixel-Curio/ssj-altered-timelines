using System.Collections.Generic;

namespace PixelCurio.AlteredTimeline
{
    public class Attack : IAction
    {
        public string Name { get; } = "Attack";
        public int ManaCost { get; } = 0;
        public void ApplyEffect(ICharacter target) => target.ReceiveDamage(10);
        public void PayCost(ICharacter source) { }
        public List<IAction> SubActions { get; } = new List<IAction>();
    }
}
