using UnityEngine;
using UnityEngine.UI;

namespace Game {
    public class CellVisualizer {
        public void UpdateVisuals(Sprite element, Image contentImage, float size) {
            RectTransform contentTransform = contentImage.rectTransform;

            contentImage.sprite = element;

            float containerWidth = size;
            float containerHeight = size;

            Vector2 spriteSize = element.bounds.size;

            float widthScale = containerWidth / spriteSize.x;
            float heightScale = containerHeight / spriteSize.y;

            float scaleFactor = Mathf.Min(widthScale, heightScale);
            contentTransform.sizeDelta = new Vector2(spriteSize.x * scaleFactor, spriteSize.y * scaleFactor);

            if (element.name == "7" || element.name == "8") {
                contentTransform.eulerAngles = new Vector3(0, 0, -90);
            }
            else {
                contentTransform.rotation=Quaternion.identity;
            }

        }
    }
}