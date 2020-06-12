using UnityEngine;
using Zenject;

namespace PixelCurio.AlteredTimeline
{
    public class MainInstaller : MonoInstaller
    {
        public Transform UiTransform;
        [Space(10)]
        public GameObject CommandsViewPrefab;
        public GameObject CommandPanelViewPrefab;
        public GameObject CommandViewPrefab;

        public override void InstallBindings()
        {
            InstallManagers();
            InstallCharacters();
            InstallMagic();
            InstallFactories();
            InstallViewControllers();
            InstallViews();
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
            Container.Bind<IAction>().To<Magic>().AsCached().WhenInjectedInto<Knight>();
            Container.Bind<IAction>().To<Item>().AsCached().WhenInjectedInto<Knight>();
            #endregion
        }

        private void InstallMagic()
        {
            Container.Bind<IAction>().To<Fireball>().AsCached().WhenInjectedInto<Magic>();
            Container.Bind<IAction>().To<IceBall>().AsCached().WhenInjectedInto<Magic>();
        }

        private void InstallViewControllers()
        {
            Container.BindInterfacesAndSelfTo<CommandsViewController>().AsSingle().NonLazy();
        }
        private void InstallViews()
        {
            Container.BindInterfacesAndSelfTo<CommandsView>()
                .FromComponentInNewPrefab(CommandsViewPrefab)
                .UnderTransform(UiTransform).AsSingle();
        }

        private void InstallFactories()
        {
            Container.BindFactory<CommandView, PlaceholderFactory<CommandView>>()
                .FromComponentInNewPrefab(CommandViewPrefab);
            Container.BindFactory<CommandPanelView, PlaceholderFactory<CommandPanelView>>()
                .FromComponentInNewPrefab(CommandPanelViewPrefab);
        }
    }
}