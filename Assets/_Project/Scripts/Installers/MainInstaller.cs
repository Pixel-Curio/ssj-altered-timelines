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
            InstallManagers();
            InstallCharacters();
            InstallFactories();
            InstallViewControllers();
        }

        private void InstallManagers()
        {
            Container.BindInterfacesAndSelfTo<CharacterManager>().AsSingle();
        }

        private void InstallCharacters()
        {
            #region Install Knight
            Container.BindInterfacesAndSelfTo<Knight>().AsSingle();
            Container.Bind<IAction>().To<Attack>().AsCached().WhenInjectedInto<Knight>();
            Container.Bind<IAction>().To<Defend>().AsCached().WhenInjectedInto<Knight>();
            Container.Bind<IAction>().To<Item>().AsCached().WhenInjectedInto<Knight>();
            #endregion
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