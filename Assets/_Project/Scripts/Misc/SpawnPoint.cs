using UnityEngine;

namespace PixelCurio.AlteredTimeline
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private Alignment _spawnAlignment;
        [SerializeField] private int _priority;
        public enum Alignment { Right, Left }
    }
}
