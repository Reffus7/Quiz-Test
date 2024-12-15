using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
namespace Game {
    public class UIController : MonoBehaviour {
        [SerializeField] private CanvasGroup endGameCanvasGroup;
        [SerializeField] private Button restartButton;

        [SerializeField] private CanvasGroup loadingScreenCanvasGroup;
        [SerializeField] private TextMeshProUGUI loadingText;

        [SerializeField] private TextMeshProUGUI findText;

        private UIAnimator uiAnimator;
        private LevelManager levelManager;

        [Inject]
        public void Construct(UIAnimator animator, LevelManager levelManager) {
            uiAnimator = animator;
            this.levelManager = levelManager;
        }

        private void Start() {
            HideEndGameUI();
            HideLoadingScreen();
            restartButton.onClick.AddListener(levelManager.RestartGame);
        }

        public void ShowEndGameUI() {
            uiAnimator.FadeInCanvas(endGameCanvasGroup, 0.7f, () => {
                restartButton.interactable = true;
            });
        }

        public void HideEndGameUI() {
            restartButton.interactable = false;
            uiAnimator.FadeOutCanvas(endGameCanvasGroup, 0.7f, () => {
                endGameCanvasGroup.gameObject.SetActive(false);
            });
        }

        public void ShowLoadingScreen(string text = "Loading...") {
            loadingText.text = text;
            uiAnimator.FadeInCanvas(loadingScreenCanvasGroup, 0.7f);
        }

        public void HideLoadingScreen() {
            uiAnimator.FadeOutCanvas(loadingScreenCanvasGroup, 0.7f, () => {
                loadingScreenCanvasGroup.gameObject.SetActive(false);
            });
        }

        public void UpdateFindText(string targetName) {
            findText.text = $"Find {targetName}";
            uiAnimator.FadeInText(findText);
        }

    }

}