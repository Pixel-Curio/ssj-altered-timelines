using System.Collections.Generic;

namespace PixelCurio.AlteredTimeline
{
    public class Item : IAction
    {
        public string Name { get; } = "Item";
        public int ManaCost { get; } = 0;
        public void ApplyEffect(ICharacter target) => target.ReceiveDamage(0);
        public List<IAction> SubActions { get; } = new List<IAction>();
    }
}
