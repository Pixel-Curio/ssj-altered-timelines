using System;
using System.Collections.Generic;

namespace PixelCurio.AlteredTimeline
{
    public interface ICharacter
    {
        string Name { get; set; }
        int CurrentHealth { get; set; }
        int MaxHealth { get; set; }
        List<IAction> Actions { get; }
        void ReceiveDamage(int damage);
        Action<float> OnHealthChange { get; set; }
        Action<float> OnManaChange { get; set; }
        void TriggerStatRefresh();
    }
}
