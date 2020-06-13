using System.Collections.Generic;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class IceBall : IAction
    {
        public string Name { get; } = "Iceball";
        public int ManaCost { get; } = 5;
        public void ApplyEffect(ICharacter target) => target.ReceiveDamage(15);
        public void PayCost(ICharacter source) => source.CurrentMana -= ManaCost;
        [Inject] public List<IAction> SubActions { get; } = new List<IAction>();
    }
}
