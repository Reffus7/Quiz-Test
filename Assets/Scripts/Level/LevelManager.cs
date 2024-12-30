using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game {
    public class LevelManager {
        private LevelDataSO[] levels;
        private SpriteSetSO[] spriteSets;
        private GridManager gridManager;
        private UIAnimator uiAnimator;
        [Inject] private UIController uiController;

        private int currentLevelIndex = 0;
        private Sprite targetElement;
        private List<Sprite> usedSprites = new();

        public LevelManager(LevelDataSO[] levels, SpriteSetSO[] spriteSets, GridManager gridManager, UIAnimator uiAnimator) {
            this.levels = levels;
            this.spriteSets = spriteSets;
            this.gridManager = gridManager;
            this.uiAnimator = uiAnimator;
        }

        public void StartGame() {
            LoadLevel(currentLevelIndex);
        }

        public void RestartGame() {
            currentLevelIndex = 0;
            usedSprites.Clear();
            uiController.HideEndGameUI();
            uiController.ShowLoadingScreen();

            RestartGameCoroutine();
        }

        private async void RestartGameCoroutine() {
            await Task.Delay(1000);
            LoadLevel(currentLevelIndex);
            uiController.HideLoadingScreen();
        }

        private void LoadNextLevel() {
            LoadLevel(currentLevelIndex + 1);
        }

        private void LoadLevel(int levelIndex) {
            if (levelIndex >= levels.Length) {
                EndGame();
                return;
            }

            currentLevelIndex = levelIndex;

            LevelDataSO levelData = levels[levelIndex];

            if (spriteSets.Length == 0) {
                Debug.LogError("Ќет доступных наборов спрайтов.");
                return;
            }

            SpriteSetSO spriteSet = GetRandomSpriteSet();
            List<Sprite> spritePool = GetFilteredSpritePool(spriteSet);

            if (spritePool.Count == 0) {
                Debug.LogError("Ќет доступных спрайтов дл€ текущего уровн€.");
                return;
            }

            targetElement = GetRandomTargetSprite(spritePool);
            spritePool = spriteSet.Sprites.ToList();
            spritePool.Remove(targetElement);

            if (spritePool.Count == 0) {
                Debug.LogError("—вободные спрайты закончились в данном наборе.");
                return;
            }

            usedSprites.Add(targetElement);

            uiController.UpdateFindText(targetElement.name);
            Sprite[] gridSprites = gridManager.GenerateGridSprites(levelData.Rows, levelData.Columns, targetElement, spritePool);
            gridManager.GenerateGrid(levelData.Rows, levelData.Columns, gridSprites, OnCellClick);


            Transform[] cellTransforms = gridManager.GetGridCells().Select(cell => cell.transform).ToArray();
            uiAnimator.BounceGridCells(cellTransforms);

        }

        private void EndGame() {
            uiController.ShowEndGameUI();
        }

        private SpriteSetSO GetRandomSpriteSet() {
            return spriteSets[UnityEngine.Random.Range(0, spriteSets.Length)];
        }

        private List<Sprite> GetFilteredSpritePool(SpriteSetSO spriteSet) {
            List<Sprite> spritePool = new List<Sprite>(spriteSet.Sprites);
            foreach (Sprite sprite in usedSprites) {
                spritePool.Remove(sprite);
            }
            return spritePool;
        }

        private Sprite GetRandomTargetSprite(List<Sprite> spritePool) {
            Sprite target = spritePool[UnityEngine.Random.Range(0, spritePool.Count)];
            return target;
        }

        private void OnCellClick(Cell cell, Sprite clickedElement) {
            cell.transform.SetAsLastSibling();

            if (clickedElement == targetElement) {
                gridManager.HandleCorrectClick(cell, LoadNextLevel);
            }
            else {
                gridManager.HandleWrongClick(cell);
            }
        }

    }
}