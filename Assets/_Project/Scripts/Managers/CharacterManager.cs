using System.Collections.Generic;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class CharacterManager : IInitializable
    {
        [Inject] private readonly Knight _knight;
        [Inject] private readonly Knight _knight2;

        [Inject] private readonly Slime _slime;
        [Inject] private readonly Slime _slime2;

        public ICharacter ActiveCharacter { get; set; }
        public List<ICharacter> SelectableCharacters { get; set; } = new List<ICharacter>();
        public List<ICharacter> SelectableEnemies { get; set; } = new List<ICharacter>();

        public void Initialize()
        {
            SelectableCharacters.Add(_knight);
            SelectableCharacters.Add(_knight2);
            ActiveCharacter = _knight;

            SelectableEnemies.Add(_slime);
            SelectableEnemies.Add(_slime2);
        }
    }
}
