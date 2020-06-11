using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class CommandsViewController : IInitializable
    {
        [Inject] private readonly PlaceholderFactory<CommandView> _commandFactory;

        public void Initialize()
        {
            var _commandView = _commandFactory.Create();
            _commandView.Name.text = "THIS IS A TEST";
            _commandView.Cursor.enabled = false;
        }
    }
}
