﻿using System.Collections.Generic;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class IceBall : IAction
    {
        [Inject] private NotificationViewController _notification;

        public string Name { get; } = "Iceball";
        public int ManaCost { get; } = 5;
        public void ApplyEffect(ICharacter source, ICharacter target)
        {
            target.ReceiveDamage(15);

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
