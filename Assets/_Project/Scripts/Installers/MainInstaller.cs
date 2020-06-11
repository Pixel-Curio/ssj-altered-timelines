using UnityEngine;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class MainInstaller : MonoInstaller
    {
        public Transform CommandTransform;
        public GameObject CommandViewPrefab;

        public override void InstallBindings()
        {
            InstallFactories();
            InstallViewControllers();
        }

        private void InstallViewControllers()
        {
            Container.BindInterfacesAndSelfTo<CommandsViewController>().AsSingle().NonLazy();
        }

        private void InstallFactories()
        {
            Container.BindFactory<CommandView, PlaceholderFactory<CommandView>>()
                .FromComponentInNewPrefab(CommandViewPrefab).UnderTransform(CommandTransform);
        }
    }
}