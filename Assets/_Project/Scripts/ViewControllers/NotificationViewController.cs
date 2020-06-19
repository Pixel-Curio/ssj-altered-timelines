using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class NotificationViewController : IInitializable, ITickable
    {
        [Inject] private NotificationView _view;

        public int RolloverCharacterSpeed { get; set; } = 100;
        public int DisplayDelay { get; set; } = 2000;

        private Task _activeTask;
        private CancellationTokenSource _tokenSource;

        public void Initialize()
        {
            _view.MiddlePanel.SetActive(false);
        }

        public void Tick()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                DisplayMessage("Not enough mana!");
            }
        }

        public void DisplayMessage(string message)
        {
            if (_activeTask != null && !_activeTask.IsCompleted)
            {
                _tokenSource.Cancel();
            }

            _tokenSource = new CancellationTokenSource();

            _activeTask = DisplayMessage(message, _tokenSource.Token);
        }

        private async Task DisplayMessage(string message, CancellationToken token)
        {
            _view.MiddlePanel.SetActive(true);

            // Need to force the text object to be generated so we have valid data to work with right from the start.
            _view.MiddleMessage.ForceMeshUpdate();

            TMP_TextInfo textInfo = _view.MiddleMessage.textInfo;

            //Turn off all characters
            int characterCount = textInfo.characterCount;
            for (int i = 0; i < characterCount; i++)
            {
                SetAlpha(textInfo, i, 0);
            }

            _view.MiddleMessage.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            _view.MiddleMessage.text = message;

            for (int i = 0; i < characterCount; i++)
            {
                SetAlpha(textInfo, i, 255);
                _view.MiddleMessage.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                await Task.Delay(RolloverCharacterSpeed, token);
                if (token.IsCancellationRequested) break;
            }

            await Task.Delay(DisplayDelay, token);

            _view.MiddlePanel.SetActive(false);
        }

        private static void SetAlpha(TMP_TextInfo textInfo, int index, byte alpha)
        {
            // Get the index of the material used by the current character.
            int materialIndex = textInfo.characterInfo[index].materialReferenceIndex;

            // Get the vertex colors of the mesh used by this text element (character or sprite).
            Color32[] newVertexColors = textInfo.meshInfo[materialIndex].colors32;

            // Get the index of the first vertex used by this text element.
            int vertexIndex = textInfo.characterInfo[index].vertexIndex;

            // Set new alpha values.
            newVertexColors[vertexIndex + 0].a = alpha;
            newVertexColors[vertexIndex + 1].a = alpha;
            newVertexColors[vertexIndex + 2].a = alpha;
            newVertexColors[vertexIndex + 3].a = alpha;
        }
    }
}
