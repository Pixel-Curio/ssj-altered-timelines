using System.Collections.Generic;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class StatusesViewController : IInitializable
    {
        [Inject] private readonly StatusesView _view;
        [Inject] private readonly PlaceholderFactory<StatusView> _statusFactory;
        [Inject] private readonly CharacterManager _characterManager;
        [Inject] private readonly InputManager _inputManager;
        [Inject] private readonly CommandsViewController _commandsViewController;

        private List<StatusView> _rightStatuses = new List<StatusView>();
        private List<StatusView> _leftStatuses = new List<StatusView>();

        private IAction _action;

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

        private void SelectNext()
        {

        }

        private void SelectPrevious()
        {

        }

        private void SelectOther()
        {

        }

        private void Confirm()
        {

        }

        private void Back()
        {

        }

        public void Activate(IAction action)
        {
            _action = action;
            _inputManager.OnDown += SelectNext;
            _inputManager.OnUp += SelectPrevious;
            _inputManager.OnRight += SelectOther;
            _inputManager.OnLeft += SelectOther;
            _inputManager.OnEnter += Confirm;
            _inputManager.OnBack += Back;
        }

        public void Deactivate(IAction action)
        {
            _action = null;
            _inputManager.OnDown -= SelectNext;
            _inputManager.OnUp -= SelectPrevious;
            _inputManager.OnRight -= SelectOther;
            _inputManager.OnLeft -= SelectOther;
            _inputManager.OnEnter -= Confirm;
            _inputManager.OnBack -= Back;
        }
    }
}
