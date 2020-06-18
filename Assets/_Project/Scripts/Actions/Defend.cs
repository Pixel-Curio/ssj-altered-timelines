using System.Collections.Generic;

namespace PixelCurio.AlteredTimeline
{
    public class Defend : IAction
    {
        public string Name { get; } = "Defend";
        public int ManaCost { get; } = 0;
        public void ApplyEffect(ICharacter target) => target.ReceiveDamage(-10);
        public bool CanPayCost(ICharacter source) => source.CurrentMana >= ManaCost;
        public void PayCost(ICharacter source) { }
        public List<IAction> SubActions { get; } = new List<IAction>();
    }
}
