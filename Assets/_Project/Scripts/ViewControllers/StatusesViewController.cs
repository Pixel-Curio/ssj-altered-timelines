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

        private int _activeIndex = -1;

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

                _rightStatuses.Add(status);
            }
        }

        private void SelectNext()
        {
            if (_activeIndex >= 0) SetSelected(_rightStatuses[_activeIndex], false);

            _activeIndex = ++_activeIndex >= _rightStatuses.Count ? 0 : _activeIndex; //Otherwise, move on to next item.

            SetSelected(_rightStatuses[_activeIndex], true);
        }

        private void SelectPrevious()
        {
            if (_activeIndex >= 0) SetSelected(_rightStatuses[_activeIndex], false);

            _activeIndex = --_activeIndex < 0 ? _rightStatuses.Count - 1 : _activeIndex;

            SetSelected(_rightStatuses[_activeIndex], true);
        }

        private void SelectOther()
        {

        }

        private void Confirm()
        {

        }

        private void Back()
        {
            Deactivate();
            if (_activeIndex >= 0) SetSelected(_rightStatuses[_activeIndex], false);
            _activeIndex = -1;
            _commandsViewController.Activate();
        }

        private static void SetSelected(StatusView view, bool isSelected) => view.SetCursorVisibility(isSelected);

        public void Activate(IAction action)
        {
            _action = action;

            _inputManager.OnDown += SelectNext;
            _inputManager.OnUp += SelectPrevious;
            _inputManager.OnRight += SelectOther;
            _inputManager.OnLeft += SelectOther;
            _inputManager.OnEnter += Confirm;
            _inputManager.OnBack += Back;

            if (_activeIndex < 0) SelectNext();
        }

        public void Deactivate()
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
