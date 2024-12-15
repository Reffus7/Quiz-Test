using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game {
    public class Cell : MonoBehaviour {
        [SerializeField] private Image background;
        [SerializeField] private Image contentImage;
        [SerializeField] private float size;

        private CellVisualizer visualizer;
        private Action onClick;

        public float GetSize() => size;

        private void Awake() {
            visualizer = new CellVisualizer();
        }

        public void Init(Sprite element, Action onCellClick) {
            SetSize(size);
            onClick = onCellClick;
            visualizer.UpdateVisuals(element, contentImage, size);
        }

        private void SetSize(float size) {
            GetComponent<RectTransform>().sizeDelta = Vector2.one * size;
            background.GetComponent<RectTransform>().sizeDelta = Vector2.one * size;
            contentImage.GetComponent<RectTransform>().sizeDelta = Vector2.one * size;
        }

        public void OnClick() {
            onClick?.Invoke();
        }
    }

}