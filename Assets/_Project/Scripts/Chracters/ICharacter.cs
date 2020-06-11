using System.Collections.Generic;

namespace PixelCurio.AlteredTimeline
{
    public interface ICharacter
    {
        List<IAction> Actions { get; }
        void ReceiveDamage(int damage);
    }
}
