using System.Collections.Generic;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class IceBall : IAction
    {
        public string Name { get; } = "IceBall";
        public int ManaCost { get; } = 5;
        public void ApplyEffect(ICharacter target) => target.ReceiveDamage(15);
        [Inject] public List<IAction> SubActions { get; } = new List<IAction>();
    }
}
