using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class CellPool {
        private Queue<Cell> queue = new Queue<Cell>();

        private Cell cellPrefab;
        private Transform gridParent;

        public CellPool(Cell cellPrefab, Transform gridParent) {
            this.cellPrefab = cellPrefab;
            this.gridParent = gridParent;
        }

        public Cell GetCell() {
            if (queue.Count > 0) {
                Cell cell = queue.Dequeue();
                cell.gameObject.SetActive(true);
                return cell;
            }
            return Object.Instantiate(cellPrefab, gridParent);
        }

        public void ReturnCell(Cell cell) {
            if (queue.Contains(cell)) return;
            cell.gameObject.SetActive(false);
            queue.Enqueue(cell);
        }

    }
}