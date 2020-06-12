using System.Collections.Generic;

namespace PixelCurio.AlteredTimeline
{
    public interface ICharacter
    {
        string Name { get; set; }
        List<IAction> Actions { get; }
        void ReceiveDamage(int damage);
    }
}
