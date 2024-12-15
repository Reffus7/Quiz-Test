using UnityEngine;
using System.Collections.Generic;

namespace Game {
    public class GridManager {
        private GridGenerator gridGenerator;
        private UIAnimator uiAnimator;
        private ParticleSystem starParticlesPrefab;

        public GridManager(GridGenerator gridGenerator, UIAnimator uiAnimator, ParticleSystem starParticlesPrefab) {
            this.gridGenerator = gridGenerator;
            this.uiAnimator = uiAnimator;
            this.starParticlesPrefab = starParticlesPrefab;
        }

        public Sprite[] GenerateGridSprites(int rows, int columns, Sprite target, List<Sprite> spritePool) {
            int gridSize = rows * columns;
            List<Sprite> sprites = new List<Sprite> { target };

            while (sprites.Count < gridSize) {
                Sprite sprite = spritePool[Random.Range(0, spritePool.Count)];
                sprites.Add(sprite);
            }

            return ShuffleArray(sprites);
        }

        public void GenerateGrid(int rows, int columns, Sprite[] sprites, System.Action<Cell, Sprite> onCellClick) {
            gridGenerator.GenerateGrid(rows, columns, sprites, onCellClick);
        }

        public void HandleCorrectClick(Cell cell, System.Action onComplete) {
            SpawnParticles(cell.transform);
            uiAnimator.BounceTransform(cell.transform.GetChild(1), onComplete);
        }

        public void HandleWrongClick(Cell cell) {
            uiAnimator.ShakeTransform(cell.transform.GetChild(1));
        }

        private void SpawnParticles(Transform cellTransform) {
            if (starParticlesPrefab != null) {
                ParticleSystem particleObject = Object.Instantiate(starParticlesPrefab, cellTransform.position - Vector3.forward, Quaternion.identity);
                particleObject.transform.SetParent(cellTransform);
                particleObject.transform.localScale = Vector3.one;

            }
        }

        private Sprite[] ShuffleArray(List<Sprite> array) {
            for (int i = array.Count - 1; i > 0; i--) {
                int randomIndex = Random.Range(0, i + 1);
                (array[i], array[randomIndex]) = (array[randomIndex], array[i]);
            }
            return array.ToArray();
        }

        public List<Cell> GetGridCells() {
            return gridGenerator.GetGridCells();
        }
    }
}