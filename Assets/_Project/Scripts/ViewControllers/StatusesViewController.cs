using System.Collections.Generic;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class StatusesViewController : IInitializable
    {
        [Inject] private readonly StatusesView _view;
        [Inject] private readonly PlaceholderFactory<StatusView> _statusFactory;
        [Inject] private readonly CharacterManager _characterManager;

        private List<StatusView> _rightStatuses = new List<StatusView>();
        private List<StatusView> _leftStatuses = new List<StatusView>();

        public void Initialize()
        {
            foreach (ICharacter character in _characterManager.SelectableCharacters)
            {
                StatusView status = _statusFactory.Create();
                status.PanelAlignment = StatusView.Alignment.Right;
                status.Character = character;
                status.SetName(character.Name);
                status.transform.SetParent(_view.RightPlatesTransform, false);
                character.OnHealthChange += status.HealthChange;
                character.OnManaChange += status.ManaChange;
                character.TriggerStatRefresh();
            }
        }
    }
}
