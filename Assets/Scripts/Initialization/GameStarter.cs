using UnityEngine;
using Zenject;

namespace Game {
    public class GameStarter : MonoBehaviour {
        private LevelManager levelManager;

        [Inject]
        public void Construct(LevelManager levelManager) {
            this.levelManager = levelManager;
        }

        void Start() {
            levelManager.StartGame();
        }
    }
}