using System.Collections.Generic;
using UnityEngine;

namespace PixelCurio.AlteredTimeline
{
    public class CommandPanelView : MonoBehaviour
    {
        public Transform RootTransform;

        public CommandPanelView ParentPanel { get; set; }

        private readonly List<CommandView> _commands = new List<CommandView>();
        private int _activeIndex = -1;
        private int _lastActiveIndex = -1;

        public CommandView GetActiveAction() => _activeIndex == -1 ? null : _commands[_activeIndex];

        public void SetChildCommand(CommandView command)
        {
            command.transform.SetParent(RootTransform, false);
            _commands.Add(command);
        }

        public void SelectNext()
        {
            if (_activeIndex >= 0) SetSelected(_commands[_activeIndex], false);

            if (_activeIndex == -1 && _lastActiveIndex != -1) _activeIndex = _lastActiveIndex; //For returning to a panel, select the last action.
            else _activeIndex = ++_activeIndex >= _commands.Count ? 0 : _activeIndex; //Otherwise, move on to next item.

            SetSelected(_commands[_activeIndex], true);
        }

        public void SelectPrevious()
        {
            if (_activeIndex >= 0) SetSelected(_commands[_activeIndex], false);
            _activeIndex = --_activeIndex < 0 ? _commands.Count - 1 : _activeIndex;
            SetSelected(_commands[_activeIndex], true);
        }

        public void ClearSelection()
        {
            if (_activeIndex >= 0) SetSelected(_commands[_activeIndex], false);
            _lastActiveIndex = _activeIndex;
            _activeIndex = -1;
        }

        public void SetPanelVisibility(bool isVisible) => RootTransform.gameObject.SetActive(isVisible);

        private static void SetSelected(CommandView view, bool isSelected) => view.Cursor.enabled = isSelected;
    }
}
