using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class CommandsViewController : IInitializable
    {
        [Inject] private readonly CommandsView _view;
        [Inject] private readonly CharacterManager _characterManager;
        [Inject] private readonly InputManager _inputManager;
        [Inject] private readonly StatusesViewController _statusesViewController;
        [Inject] private readonly PlaceholderFactory<CommandView> _commandFactory;
        [Inject] private readonly PlaceholderFactory<CommandPanelView> _commandPanelFactory;

        private CommandPanelView _activePanelView;

        public void Initialize()
        {
            CommandPanelView commandPanelView = CreatePanel();

            _activePanelView = commandPanelView;

            //Register root actions
            foreach (IAction action in _characterManager.ActiveCharacter.Actions)
            {
                CommandView commandView = _commandFactory.Create();
                commandView.Name.text = action.Name;
                commandView.Cursor.enabled = false;
                commandView.Action = action;
                commandPanelView.SetChildCommand(commandView);

                //Register sub actions (eg magic, items, etc)
                if (action.SubActions == null || action.SubActions.Count == 0) continue;

                CommandPanelView subCommandPanel = CreatePanel(commandPanelView);
                commandView.ChildPanel = subCommandPanel;

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

        private void SelectCommand()
        {
            if (_activePanelView.GetActiveAction() == null) throw new WarningException("Select command was hit before an item has been selected.");
            if (_activePanelView.GetActiveAction().ChildPanel == null)
            {
                Deactivate();
                _statusesViewController.Activate(_activePanelView.GetActiveAction().Action);
                return;
            }
            _activePanelView = _activePanelView.GetActiveAction().ChildPanel;
            _activePanelView.SetPanelVisibility(true);
            _activePanelView.ParentPanel.ClearSelection();
            if (_activePanelView.GetActiveAction() == null) _activePanelView.SelectNext(); //Make sure we select the first item if nothing has been selected yet.
        }

        private void BackCommand()
        {
            if (_activePanelView.ParentPanel == null) return; //Already at root;
            _activePanelView.ClearSelection();
            _activePanelView.SetPanelVisibility(false);
            _activePanelView = _activePanelView.ParentPanel;
            _activePanelView.SelectNext();
        }

        private void NextCommand() => _activePanelView.SelectNext();

        private void PreviousCommand() => _activePanelView.SelectPrevious();

        private CommandPanelView CreatePanel(CommandPanelView parentPanel = null)
        {
            CommandPanelView panelView = _commandPanelFactory.Create();
            panelView.transform.SetParent(_view.CommandPlatesTransform, false);

            if (parentPanel == null) return panelView;

            panelView.ParentPanel = parentPanel;
            panelView.SetPanelVisibility(false);

            return panelView;
        }

        public void Activate()
        {
            _inputManager.OnRight += SelectCommand;
            _inputManager.OnEnter += SelectCommand;
            _inputManager.OnLeft += BackCommand;
            _inputManager.OnBack += BackCommand;
            _inputManager.OnDown += NextCommand;
            _inputManager.OnUp += PreviousCommand;
        }

        private void Deactivate()
        {
            _inputManager.OnRight -= SelectCommand;
            _inputManager.OnEnter -= SelectCommand;
            _inputManager.OnLeft -= BackCommand;
            _inputManager.OnBack -= BackCommand;
            _inputManager.OnDown -= NextCommand;
            _inputManager.OnUp -= PreviousCommand;
        }

        private async Task DelayedSelect()
        {
            await Task.Delay(500);
            _activePanelView.SelectNext();
            Activate();
        }
    }
}
