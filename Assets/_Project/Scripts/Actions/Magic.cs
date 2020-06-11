using System.Collections.Generic;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class Magic : IAction
    {
        public string Name { get; } = "Magic";
        public int ManaCost { get; } = 0;
        public void ApplyEffect(ICharacter target) => target.ReceiveDamage(-10);
        [Inject] public List<IAction> SubActions { get; } = new List<IAction>();
    }
}
