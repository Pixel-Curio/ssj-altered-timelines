using System.Collections.Generic;
using Cinemachine;
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
        [Inject] private readonly CursorView _cursorView;
        [Inject(Id = "FollowCam")] private readonly CinemachineVirtualCamera _followCamera;
        [Inject(Id = "BattlegroundCam")] private readonly CinemachineVirtualCamera _battlegroundCamera;

        private List<StatusView> _rightStatuses = new List<StatusView>();
        private List<StatusView> _leftStatuses = new List<StatusView>();
        private List<StatusView> _activeList;

        private int _activeIndex = -1;

        private IAction _action;

        public void Initialize()
        {
            foreach (ICharacter character in _characterManager.SelectableCharacters)
                AddCharacter(character, StatusView.Alignment.Right);

            foreach (ICharacter character in _characterManager.SelectableEnemies)
                AddCharacter(character, StatusView.Alignment.Left);

            _activeList = _rightStatuses;
        }

        private void AddCharacter(ICharacter character, StatusView.Alignment alignment)
        {
            StatusView status = _statusFactory.Create();
            status.PanelAlignment = alignment;
            status.Character = character;
            status.SetName(character.Name);
            status.transform.SetParent(alignment == StatusView.Alignment.Right ?
                _view.RightPlatesTransform : _view.LeftPlatesTransform, false);

            character.OnHealthChange += status.HealthChange;
            character.OnManaChange += status.ManaChange;
            character.TriggerStatRefresh();

            if (alignment == StatusView.Alignment.Right)
                _rightStatuses.Add(status);
            else if (alignment == StatusView.Alignment.Left)
                _leftStatuses.Add(status);
        }

        private void SelectNext()
        {
            if (_activeIndex >= 0) SetSelected(_activeList[_activeIndex], false);

            _activeIndex = ++_activeIndex >= _activeList.Count ? 0 : _activeIndex; //Otherwise, move on to next item.

            SetSelected(_activeList[_activeIndex], true);
        }

        private void SelectPrevious()
        {
            if (_activeIndex >= 0) SetSelected(_activeList[_activeIndex], false);

            _activeIndex = --_activeIndex < 0 ? _activeList.Count - 1 : _activeIndex;

            SetSelected(_activeList[_activeIndex], true);
        }

        private void SelectOther()
        {
            ClearSelection();

            if (_activeList == _rightStatuses) _activeList = _leftStatuses;
            else if (_activeList == _leftStatuses) _activeList = _rightStatuses;

            SelectNext();
        }

        private void Confirm()
        {
            _action.PayCost(_characterManager.ActiveCharacter);
            _action.ApplyEffect(_activeList[_activeIndex].Character);
        }

        private void Back()
        {
            Deactivate();
            ClearSelection();
            _commandsViewController.Activate();
        }

        private void SetSelected(StatusView view, bool isSelected)
        {
            view.SetCursorVisibility(isSelected);
            _cursorView.Renderer.enabled = true;
            _cursorView.transform.position = view.Character.View.IconTransform.position;
            _followCamera.LookAt = view.Character.View.IconTransform;
            _followCamera.Follow = view.Character.View.IconTransform;
            _battlegroundCamera.Priority = 0;
        }

        private void ClearSelection()
        {
            if (_activeIndex >= 0) SetSelected(_activeList[_activeIndex], false);
            _cursorView.Renderer.enabled = false;
            _battlegroundCamera.Priority = 15;
            _activeIndex = -1;
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
