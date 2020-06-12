using System.Collections.Generic;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class Knight : ICharacter
    {
        [Inject] public List<IAction> Actions { get; set; }

        public string Name { get; set; } = "Knight";

        public void ReceiveDamage(int damage)
        {
            throw new System.NotImplementedException();
        }
    }
}

