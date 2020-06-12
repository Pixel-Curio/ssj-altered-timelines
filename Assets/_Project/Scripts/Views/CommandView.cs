using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCurio.AlteredTimeline
{
    public class CommandView : MonoBehaviour
    {
        public Image Cursor;
        public TextMeshProUGUI Name;
        public IAction Action { get; set; }
        public CommandPanelView ChildPanel { get; set; }
    }
}
