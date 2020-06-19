using System.Collections.Generic;
using UnityEditor.SceneManagement;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class CharacterManager : IInitializable
    {
        [Inject] private readonly Knight _knight;
        [Inject] private readonly Knight _knight2;

        [Inject] private readonly Slime _slime;
        [Inject] private readonly Slime _slime2;

        private int _activeIndex = -1;

        public ICharacter ActiveCharacter => _activeIndex < 0 ? null : TurnOrder[_activeIndex];
        public List<ICharacter> SelectableCharacters { get; set; } = new List<ICharacter>();
        public List<ICharacter> SelectableEnemies { get; set; } = new List<ICharacter>();
        public List<ICharacter> TurnOrder { get; set; } = new List<ICharacter>();

        public void Initialize()
        {
            SelectableCharacters.Add(_knight.Initialize());
            SelectableCharacters.Add(_knight2.Initialize());

            SelectableEnemies.Add(_slime.Initialize());
            SelectableEnemies.Add(_slime2.Initialize());

            TurnOrder.AddRange(SelectableCharacters);
            //TurnOrder.AddRange(SelectableEnemies);

            _activeIndex = 0;
        }

        public void SelectNext()
        {
            if (_activeIndex >= 0) ActiveCharacter.CommandPanelView.SetPanelVisibility(false);

            if (++_activeIndex >= TurnOrder.Count)
            {
                _activeIndex = 0;
                foreach (ICharacter enemy in SelectableEnemies)
                {

                }
            }

            //_activeIndex = ++_activeIndex >= TurnOrder.Count ? 0 : _activeIndex;
            ActiveCharacter.CommandPanelView.SetPanelVisibility(true);
        }
    }
}
