using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class GridGenerator : MonoBehaviour {
        [SerializeField] private Cell cellPrefab;
        [SerializeField] private Transform gridParent;

        private List<Cell> cells = new List<Cell>();

        public void GenerateGrid(int rows, int columns, Sprite[] sprites, Action<Cell, Sprite> onCellClick) {
            ClearGrid();

            Sprite[] elements = sprites;
            int elementIndex = 0;

            float cellWidth = cellPrefab.GetSize();
            float cellHeight = cellPrefab.GetSize();

            float gridWidth = columns * cellWidth;
            float gridHeight = rows * cellHeight;

            float offsetX = -gridWidth / 2 + cellWidth / 2;
            float offsetY = gridHeight / 2 - cellHeight / 2;

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    Cell cell = Instantiate(cellPrefab, gridParent);
                    RectTransform rectTransform = cell.GetComponent<RectTransform>();

                    rectTransform.anchoredPosition = new Vector2(
                        j * cellWidth + offsetX,
                        -i * cellHeight + offsetY
                    );

                    if (elementIndex < elements.Length) {
                        Sprite element = elements[elementIndex];
                        cell.Init(element, () => onCellClick(cell, element));
                        elementIndex++;
                    }

                    cells.Add(cell);
                }
            }

        }

        private void ClearGrid() {
            foreach (Transform child in gridParent) {
                child.DOKill();
                Destroy(child.gameObject);
                cells.Clear();
            }
        }

        public List<Cell> GetGridCells() {
            return cells;
        }

        public Transform GetGridParent() {
            return gridParent;
        }

    }

}