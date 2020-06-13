using System.Collections.Generic;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class Fireball : IAction
    {
        public string Name { get; } = "Fireball";
        public int ManaCost { get; } = 10;
        public void ApplyEffect(ICharacter target) => target.ReceiveDamage(30);

        public void PayCost(ICharacter source)
        {
            source.CurrentMana -= ManaCost;
            source.TriggerStatRefresh();
        }

        [Inject] public List<IAction> SubActions { get; } = new List<IAction>();
    }
}
