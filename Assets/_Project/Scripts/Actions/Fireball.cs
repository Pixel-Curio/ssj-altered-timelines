using System.Collections.Generic;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class Fireball : IAction
    {
        [Inject] private NotificationViewController _notification;

        public string Name { get; } = "Fireball";
        public int ManaCost { get; } = 10;
        public void ApplyEffect(ICharacter source, ICharacter target)
        {
            target.ReceiveDamage(20);

            _notification.DisplayMessage($"{source.Name} used {Name} on {target.Name}!");
        }

        public bool CanPayCost(ICharacter source) => source.CurrentMana >= ManaCost;
        public void PayCost(ICharacter source)
        {
            source.CurrentMana -= ManaCost;
            source.TriggerStatRefresh();
        }

        [Inject] public List<IAction> SubActions { get; } = new List<IAction>();
    }
}
