using System.Collections.Generic;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class Magic : IAction
    {
        public string Name { get; } = "Magic";
        public int ManaCost { get; } = 0;
        public void ApplyEffect(ICharacter source, ICharacter target) => target.ReceiveDamage(-10);
        public bool CanPayCost(ICharacter source) => source.CurrentMana >= ManaCost;
        public void PayCost(ICharacter source) { }
        [Inject] public List<IAction> SubActions { get; } = new List<IAction>();
    }
}
