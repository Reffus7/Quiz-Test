using DG.Tweening;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

namespace Game {
    public class UIAnimator {

        // show canvas
        public void FadeInCanvas(CanvasGroup canvasGroup, float duration, Action onComplete = null) {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.alpha = 0f;

            canvasGroup.DOFade(1f, duration)
                .SetEase(Ease.InOutQuad)
                .OnStart(() => {
                    canvasGroup.transform.localScale = Vector3.one * 0.8f;
                    canvasGroup.transform.DOScale(Vector3.one, duration).SetEase(Ease.OutElastic);
                })
                .OnComplete(() => onComplete?.Invoke());
        }

        // hide canvas
        public void FadeOutCanvas(CanvasGroup canvasGroup, float duration, Action onComplete = null) {
            canvasGroup.DOFade(0f, duration)
                .SetEase(Ease.InOutQuad)
                .OnStart(() => {
                    canvasGroup.transform.DOScale(Vector3.one * 1.1f, duration / 2).SetEase(Ease.OutQuad)
                        .OnComplete(() => canvasGroup.transform.DOScale(Vector3.one * 0.8f, duration / 2).SetEase(Ease.InBack));
                })
                .OnComplete(() => {
                    canvasGroup.gameObject.SetActive(false);
                    onComplete?.Invoke();
                });
        }

        // text appears 
        public void FadeInText(TextMeshProUGUI text, float duration = 1f) {
            text.alpha = 0f;
            text.DOFade(1f, duration)
                .SetEase(Ease.InOutQuad)
                .OnStart(() => {
                    text.transform.localScale = Vector3.one * 1.2f;
                    text.transform.DOScale(Vector3.one, duration).SetEase(Ease.OutElastic);
                    text.transform.DOShakePosition(duration, new Vector3(5f, 5f, 0), 10, 90, false, true);

                });
        }

        // cells appear 
        public void BounceGridCells(Transform[] cellTransforms, float duration = 0.5f, float delay = 0.1f) {
            for (int i = 0; i < cellTransforms.Length; i++) {
                Transform cell = cellTransforms[i];
                if (cell == null) continue;

                cell.localScale = Vector3.zero;
                cell.DOScale(Vector3.one, duration)
                    .SetEase(Ease.OutBounce)
                    .SetDelay(i * delay)
                    .OnStart(() => {
                        cell.localRotation = Quaternion.Euler(0, 0, 90f);
                        cell.DORotate(Vector3.zero, duration).SetEase(Ease.OutElastic);
                    });
            }
        }


        // right answer 
        public void BounceTransform(Transform cellContent, Action onComplete = null) {
            float duration = 1f;

            cellContent.localScale = Vector3.one * 0.8f;
            cellContent.DOScale(Vector3.one * 1.3f, duration)
                .SetEase(Ease.OutElastic)
                .OnComplete(() => {
                    cellContent.DOScale(Vector3.one, duration / 2)
                        .SetEase(Ease.InOutQuad)
                        .OnComplete(() => onComplete?.Invoke());
                });
        }

        // wrong answer 
        public void ShakeTransform(Transform cellContent) {
            float duration = 0.3f;

            cellContent.DOKill();

            Vector3 originalPosition = cellContent.localPosition;
            Vector3 originalRotation = cellContent.localEulerAngles;

            cellContent.DOLocalMoveX(originalPosition.x + 15f, duration)
                .SetEase(Ease.OutQuad)
                .SetLoops(2, LoopType.Yoyo)
                .OnKill(() => cellContent.localPosition = originalPosition);

            cellContent.DORotate(originalRotation + new Vector3(0, 0, 10f), duration)
                .SetEase(Ease.OutQuad)
                .SetLoops(2, LoopType.Yoyo)
                .OnKill(() => {
                    if (cellContent.localEulerAngles != Vector3.zero && cellContent.localEulerAngles != new Vector3(0, 0, 270)) {
                        cellContent.localEulerAngles = originalRotation;
                    }
                });

            Image image = cellContent.GetComponent<Image>();
            image.DOKill();
            image.DOColor(new Color(1, .5f, .5f), duration)
                .SetLoops(2, LoopType.Yoyo)
                .OnKill(() => image.color = Color.white);
        }
    }
}

