using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class Knight : ICharacter
    {
        [Inject] public List<IAction> Actions { get; set; }
        [Inject] public CharacterView View { get; set; }
        [Inject] private List<SpawnPoint> _spawnPoints { get; set; }

        public string Name { get; set; } = "Knight";
        public int CurrentHealth { get; set; } = 100;
        public int MaxHealth { get; set; } = 100;
        public int CurrentMana { get; set; } = 100;
        public int MaxMana { get; set; } = 100;

        public Knight Initialize()
        {
            SpawnPoint spawnPoint = _spawnPoints.OrderByDescending(x => x.GetPriority())
                .FirstOrDefault(x => !x.Used && x.GetAlignment() == SpawnPoint.Alignment.Right);

            if (spawnPoint == null) throw new Exception("No usable spawn point found for character.");

            View.transform.position = spawnPoint.transform.position;
            spawnPoint.Used = true;

            return this;
        }

        public void ReceiveDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth < 0) CurrentHealth = 0;

            TriggerStatRefresh();
        }

        public Action<float> OnHealthChange { get; set; }

        public Action<float> OnManaChange { get; set; }

        public void TriggerStatRefresh()
        {
            OnHealthChange?.Invoke((float)CurrentHealth / MaxHealth);
            OnManaChange?.Invoke((float)CurrentMana / MaxMana);
        }
    }
}

