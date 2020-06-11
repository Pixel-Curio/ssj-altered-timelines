using System;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class CommandsViewController : IInitializable
    {
        [Inject] private readonly CharacterManager _characterManager;
        [Inject] private readonly PlaceholderFactory<CommandView> _commandFactory;

        public void Initialize()
        {
            foreach (IAction action in _characterManager.ActiveCharacter.Actions)
            {
                var commandView = _commandFactory.Create();
                commandView.Name.text = action.Name;
                commandView.Cursor.enabled = false;
            }
        }
    }
}
