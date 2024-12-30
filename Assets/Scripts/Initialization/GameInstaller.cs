using Zenject;
using UnityEngine;

namespace Game {
    public class GameInstaller : MonoInstaller {
        [SerializeField] private ParticleSystem starParticlesPrefab;
        [SerializeField] private UIController uiController;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private Transform gridParent;
        [SerializeField] private Cell cellPrefab;
        [SerializeField] private LevelDataSO[] levels;
        [SerializeField] private SpriteSetSO[] spriteSets;

        public override void InstallBindings() {
            Container.Bind<UIController>().FromInstance(uiController).AsSingle();
            Container.Bind<Cell>().FromInstance(cellPrefab).AsSingle();
            Container.Bind<SoundManager>().FromInstance(soundManager).AsSingle();
            Container.Bind<LevelDataSO[]>().FromInstance(levels).AsSingle();
            Container.Bind<SpriteSetSO[]>().FromInstance(spriteSets).AsSingle();

            Container.Bind<Transform>().WithId("GridParent").FromInstance(gridParent).AsSingle();
            Container.Bind<ParticleSystem>().WithId("StarParticles").FromInstance(starParticlesPrefab).AsSingle();

            Container.Bind<UIAnimator>().AsSingle();
            Container.Bind<GridGenerator>().AsSingle();
            Container.Bind<GridManager>().AsSingle();
            Container.Bind<LevelManager>().AsSingle();

        }
    }
}