using System.Collections.Generic;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class Character
    {
        [Inject] private List<IAction> _actions;
    }
}

