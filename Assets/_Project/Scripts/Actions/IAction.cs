using System.Collections.Generic;

namespace PixelCurio.AlteredTimeline
{
    public interface IAction
    {
        string Name { get; }
        int ManaCost { get; }
        void ApplyEffect(ICharacter target);
        void PayCost(ICharacter source);
        List<IAction> SubActions { get; }
    }
}
