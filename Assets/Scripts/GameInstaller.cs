using Zenject;
using UnityEngine;

namespace Game {
    public class GameInstaller : MonoInstaller {
        [SerializeField] private GridGenerator gridGenerator;
        [SerializeField] private ParticleSystem starParticlesPrefab;
        [SerializeField] private UIController uiController;
        [SerializeField] private LevelDataSO[] levels;
        [SerializeField] private SpriteSetSO[] spriteSets;

        public override void InstallBindings() {
            UIAnimator uiAnimator = new UIAnimator();
            Container.Bind<UIAnimator>().FromInstance(uiAnimator).AsSingle();

            Container.Bind<UIController>().FromInstance(uiController).AsSingle();
            Container.Bind<GridGenerator>().FromInstance(gridGenerator).AsSingle();
            Container.Bind<ParticleSystem>().FromInstance(starParticlesPrefab).AsSingle();

            GridManager gridManager = new GridManager(gridGenerator, uiAnimator, starParticlesPrefab);
            Container.Bind<GridManager>().FromInstance(gridManager).AsSingle();

            Container.Bind<LevelManager>()
                .AsSingle()
                .WithArguments(levels, spriteSets, gridManager, uiController, uiAnimator);

        }
    }
}