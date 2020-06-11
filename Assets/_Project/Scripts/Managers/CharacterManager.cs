using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class CharacterManager : IInitializable
    {
        [Inject] private readonly Knight _knight;

        public ICharacter ActiveCharacter { get; set; }

        public void Initialize()
        {
            ActiveCharacter = _knight;
        }
    }
}
