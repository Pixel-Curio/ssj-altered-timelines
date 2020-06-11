using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class CommandsViewController : IInitializable, ITickable
    {
        [Inject] private readonly CharacterManager _characterManager;
        [Inject] private readonly PlaceholderFactory<CommandView> _commandFactory;

        private List<(CommandView view, IAction action)> _actions = new List<(CommandView, IAction)>();
        private int _activeIndex = -1;

        public IAction GetActiveAction() => _activeIndex == -1 ? null : _actions[_activeIndex].Item2;

        public void Initialize()
        {
            foreach (IAction action in _characterManager.ActiveCharacter.Actions)
            {
                CommandView commandView = _commandFactory.Create();
                commandView.Name.text = action.Name;
                commandView.Cursor.enabled = false;

                if (action.SubActions.Count > 0) Debug.Log(action.SubActions[0].Name);

                _actions.Add((commandView, action));
            }

            SelectNext();
        }

        private void SelectNext()
        {
            if (_activeIndex >= 0) SetSelected(_actions[_activeIndex].view, false);
            _activeIndex = ++_activeIndex >= _actions.Count ? 0 : _activeIndex;
            SetSelected(_actions[_activeIndex].view, true);
        }

        private void SelectPrevious()
        {
            if (_activeIndex >= 0) SetSelected(_actions[_activeIndex].view, false);
            _activeIndex = --_activeIndex < 0 ? _actions.Count - 1 : _activeIndex;
            SetSelected(_actions[_activeIndex].view, true);
        }

        private static void SetSelected(CommandView view, bool isSelected) => view.Cursor.enabled = isSelected;

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow)) SelectNext();
            if (Input.GetKeyDown(KeyCode.UpArrow)) SelectPrevious();
        }
    }
}
