using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class CommandsViewController : IInitializable, ITickable
    {
        [Inject] private readonly CommandsView _view;
        [Inject] private readonly CharacterManager _characterManager;
        [Inject] private readonly PlaceholderFactory<CommandView> _commandFactory;
        [Inject] private readonly PlaceholderFactory<CommandPanelView> _commandPanelFactory;

        private CommandPanelView _activePanelView;

        public void Initialize()
        {
            CommandPanelView commandPanelView = _commandPanelFactory.Create();
            commandPanelView.transform.SetParent(_view.CommandPlatesTransform, false);

            _activePanelView = commandPanelView;

            foreach (IAction action in _characterManager.ActiveCharacter.Actions)
            {
                CommandView commandView = _commandFactory.Create();
                commandView.Name.text = action.Name;
                commandView.Cursor.enabled = false;
                commandView.Action = action;
                commandPanelView.SetChildCommand(commandView);

                if (action.SubActions == null || action.SubActions.Count == 0) continue;

                CommandPanelView subCommandPanel = _commandPanelFactory.Create();
                subCommandPanel.transform.SetParent(_view.CommandPlatesTransform, false);
                commandView.ChildPanel = subCommandPanel;
                subCommandPanel.ParentPanel = commandPanelView;

                foreach (IAction subAction in action.SubActions)
                {
                    CommandView subCommandView = _commandFactory.Create();
                    subCommandView.Name.text = subAction.Name;
                    subCommandView.Cursor.enabled = false;
                    subCommandView.Action = subAction;
                    subCommandPanel.SetChildCommand(subCommandView);
                }
            }

            _ = DelayedSelect();
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Return)) SelectCommand();
            if (Input.GetKeyDown(KeyCode.Escape)) BackCommand();
            if (Input.GetKeyDown(KeyCode.DownArrow)) _activePanelView.SelectNext();
            if (Input.GetKeyDown(KeyCode.UpArrow)) _activePanelView.SelectPrevious();
        }

        private void SelectCommand()
        {
            if(_activePanelView.GetActiveAction() == null) throw new WarningException("Select command was hit before an item has been selected.");
            if (_activePanelView.GetActiveAction().ChildPanel == null) return; //TODO: Replace this with command action;
            _activePanelView = _activePanelView.GetActiveAction().ChildPanel;
            if (_activePanelView.GetActiveAction() == null) _activePanelView.SelectNext(); //Make sure we select the first item if nothing has been selected yet.
        }

        private void BackCommand()
        {
            if (_activePanelView.ParentPanel == null) return; //Already at root;
            _activePanelView = _activePanelView.ParentPanel;
        }

        private async Task DelayedSelect()
        {
            await Task.Delay(500);
            _activePanelView.SelectNext();
        }
    }
}
