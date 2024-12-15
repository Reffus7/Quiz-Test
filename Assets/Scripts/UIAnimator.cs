using DG.Tweening;
using UnityEngine;
using TMPro;
using System;

namespace Game {
    public class UIAnimator {

        // show canvas
        public void FadeInCanvas(CanvasGroup canvasGroup, float duration, Action onComplete = null) {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1f, duration).SetEase(Ease.InOutQuad).OnComplete(() => onComplete?.Invoke());
        }

        //hide canvas
        public void FadeOutCanvas(CanvasGroup canvasGroup, float duration, Action onComplete = null) {
            canvasGroup.DOFade(0f, duration).SetEase(Ease.InOutQuad).OnComplete(() => {
                canvasGroup.gameObject.SetActive(false);
                onComplete?.Invoke();
            });
        }
        // text appears
        public void FadeInText(TextMeshProUGUI text, float duration = 1f) {
            text.alpha = 0f;
            text.DOFade(1f, duration).SetEase(Ease.InOutQuad);
        }
        // cells appears
        public void BounceGridCells(Transform[] cellTransforms, float duration = 0.5f, float delay = 0.1f) {
            for (int i = 0; i < cellTransforms.Length; i++) {
                Transform cell = cellTransforms[i];
                if (cell == null) continue;
                cell.localScale = Vector3.zero;
                cell.DOScale(Vector3.one, duration)
                    .SetEase(Ease.OutBounce)
                    .SetDelay(i * delay);
            }
        }
        //right answer
        public void BounceTransform(Transform cellContent, Action onComplete = null) {
            if (cellContent == null) return;

            float bounceDuration = 0.5f;

            cellContent.DOKill();
            cellContent.localScale = Vector3.one;
            cellContent.DOScale(Vector3.one * 1.2f, bounceDuration)
                .SetEase(Ease.OutBounce)
                .OnComplete(() => {
                    cellContent.DOScale(Vector3.one, bounceDuration / 2).SetEase(Ease.InOutQuad)
                        .OnComplete(() => onComplete?.Invoke());
                });
        }

        //wrong answer
        public void ShakeTransform(Transform cellContent) {
            if (cellContent == null) return;

            float shakeDistance = 20f;
            float shakeDuration = 0.5f;

            cellContent.DOLocalMoveX(shakeDistance, shakeDuration)
                .SetEase(Ease.InBounce)
                .SetLoops(2, LoopType.Yoyo);
        }
    }



}

