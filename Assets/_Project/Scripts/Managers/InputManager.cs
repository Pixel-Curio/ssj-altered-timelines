using System;
using UnityEngine;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class InputManager : ITickable
    {
        public Action OnDown;
        public Action OnUp;
        public Action OnRight;
        public Action OnLeft;
        public Action OnEnter;
        public Action OnBack;

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow)) OnDown?.Invoke();
            if (Input.GetKeyDown(KeyCode.UpArrow)) OnUp?.Invoke();
            if (Input.GetKeyDown(KeyCode.RightArrow)) OnRight?.Invoke();
            if (Input.GetKeyDown(KeyCode.LeftArrow)) OnLeft?.Invoke();

            if (Input.GetKeyDown(KeyCode.Return)) OnEnter?.Invoke();
            if (Input.GetKeyDown(KeyCode.Escape)) OnBack?.Invoke();
        }
    }
}
