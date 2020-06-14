using UnityEngine;

namespace PixelCurio.AlteredTimeline
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private Alignment _spawnAlignment;
        [SerializeField] private int _priority;
        public int GetPriority() => _priority;
        public Alignment GetAlignment() => _spawnAlignment;

        public bool Used { get; set; }

        public enum Alignment { Right, Left }
    }
}
