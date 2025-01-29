using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game {
    public class GridGenerator {
        private Cell cellPrefab;
        private Transform gridParent;

        private CellPool cellPool;

        private List<Cell> cells = new List<Cell>();

        public GridGenerator(Cell cellPrefab, [Inject(Id = "GridParent")]Transform gridParent) {
            this.cellPrefab = cellPrefab;
            this.gridParent = gridParent;
            cellPool=new CellPool(cellPrefab, gridParent);
        }

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
                    Cell cell = cellPool.GetCell();
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
                child.GetChild(1).DOKill();
                cellPool.ReturnCell(child.GetComponent<Cell>());
            }
            cells.Clear();
        }

        public List<Cell> GetGridCells() {
            return cells;
        }

    }

}