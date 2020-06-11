﻿namespace PixelCurio.AlteredTimeline
{
    public class Item : IAction
    {
        public string Name { get; } = "Item";
        public int ManaCost { get; } = 0;
        public void ApplyEffect(ICharacter target) => target.ReceiveDamage(0);
    }
}
