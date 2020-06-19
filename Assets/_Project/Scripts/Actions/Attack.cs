using System.Collections.Generic;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class Attack : IAction
    {
        [Inject] private NotificationViewController _notification;

        public string Name { get; } = "Attack";
        public int ManaCost { get; } = 0;

        public void ApplyEffect(ICharacter source, ICharacter target)
        {
            target.ReceiveDamage(10);
            _notification.DisplayMessage($"{source.Name} used {Name} on {target.Name}!");
        }

        public bool CanPayCost(ICharacter source) => source.CurrentMana >= ManaCost;
        public void PayCost(ICharacter source) { }

        public List<IAction> SubActions { get; } = new List<IAction>();
    }
}
