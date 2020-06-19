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
        public GameObject StatusViewPrefab;
        [Space(10)]
        public GameObject CursorViewPrefab;
        [Space(10)]
        public GameObject KnightPrefab;
        public GameObject ThiefPrefab;
        public GameObject RangerPrefab;
        [Space(10)]
        public GameObject SlimePrefab;

        public override void InstallBindings()
        {
            InstallManagers();
            InstallCharacters();
            InstallEnemies();
            InstallMagic();
            InstallFactories();
            InstallViewControllers();
            InstallViews();
        }

        private void InstallManagers()
        {
            Container.BindInterfacesAndSelfTo<CharacterManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
        }

        private void InstallCharacters()
        {
            #region Install Knight
            Container.BindInterfacesAndSelfTo<Knight>().AsTransient();
            Container.BindInterfacesAndSelfTo<CharacterView>()
                .FromComponentInNewPrefab(KnightPrefab)
                .WhenInjectedInto<Knight>();
            Container.Bind<IAction>().To<Attack>().AsCached().WhenInjectedInto<Knight>();
            Container.Bind<IAction>().To<Defend>().AsCached().WhenInjectedInto<Knight>();
            Container.Bind<IAction>().To<Magic>().AsCached().WhenInjectedInto<Knight>();
            Container.Bind<IAction>().To<Item>().AsCached().WhenInjectedInto<Knight>();
            #endregion
        }

        private void InstallEnemies()
        {
            Container.BindInterfacesAndSelfTo<Slime>().AsTransient();
            Container.BindInterfacesAndSelfTo<CharacterView>()
                .FromComponentInNewPrefab(SlimePrefab)
                .WhenInjectedInto<Slime>();
        }

        private void InstallMagic()
        {
            Container.Bind<IAction>().To<Fireball>().AsCached().WhenInjectedInto<Magic>();
            Container.Bind<IAction>().To<IceBall>().AsCached().WhenInjectedInto<Magic>();
        }

        private void InstallViewControllers()
        {
            Container.BindInterfacesAndSelfTo<CommandsViewController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StatusesViewController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<NotificationViewController>().AsSingle().NonLazy();
        }
        private void InstallViews()
        {
            Container.BindInterfacesAndSelfTo<CursorView>()
                .FromComponentInNewPrefab(CursorViewPrefab)
                .UnderTransform(UiTransform).AsSingle();

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

            Container.BindFactory<StatusView, PlaceholderFactory<StatusView>>()
                .FromComponentInNewPrefab(StatusViewPrefab);
        }
    }
}