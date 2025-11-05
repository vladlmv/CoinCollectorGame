using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace _Project.Scripts.MVP
{
    public class GameView : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI coinCounterText;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private GameObject victoryImage;
        [SerializeField] private TextMeshProUGUI victoryText;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;
        
        public event System.Action RestartButtonClicked;
        public event System.Action MenuButtonClicked;

        private bool _isTimerRunning = true;

        public void Initialize()
        {
            // Настраиваем кнопки
            if (restartButton != null)
            {
                restartButton.onClick.AddListener(() => RestartButtonClicked?.Invoke());
                restartButton.gameObject.SetActive(false);
            }
            
            if (menuButton != null)
            {
                menuButton.onClick.AddListener(() => MenuButtonClicked?.Invoke());
                menuButton.gameObject.SetActive(false);
            }

            // Скрываем панель победы
            if (victoryImage != null)
                victoryImage.SetActive(false);

            // Устанавливаем начальное значение таймера
            if (timerText != null)
                timerText.text = "00:00";
        }

        public void UpdateCoinCounter(int collected, int total)
        {
            if (coinCounterText != null)
            {
                coinCounterText.text = $"Монеты: <color=#FFD700>{collected}</color>/{total}";
            }
        }

        public void UpdateTimer(float gameTime)
        {
            if (timerText != null)
            {
                int minutes = Mathf.FloorToInt(gameTime / 60);
                int seconds = Mathf.FloorToInt(gameTime % 60);
                timerText.text = $"{minutes:00}:{seconds:00}";
            }
        }

        public void ShowVictoryScreen(float gameTime)
        {
            _isTimerRunning = false;
            
            if (victoryImage != null)
            {
                victoryImage.SetActive(true);
                
                if (victoryText != null)
                {
                    int minutes = Mathf.FloorToInt(gameTime / 60);
                    int seconds = Mathf.FloorToInt(gameTime % 60);
                    victoryText.text = $"ПОБЕДА!\n<size=24>Время: {minutes:00}:{seconds:00}</size>";
                }

                if (restartButton != null) 
                    restartButton.gameObject.SetActive(true);
                if (menuButton != null) 
                    menuButton.gameObject.SetActive(true);
                
                // Запускаем анимацию
                StartCoroutine(FadeInVictoryMessage());
            }
        }

        public void SetTimerRunning(bool isRunning)
        {
            _isTimerRunning = isRunning;
        }

        // Анимация плавного появления панели победы
        private IEnumerator FadeInVictoryMessage()
        {
            Image background = victoryImage.GetComponent<Image>();
            TextMeshProUGUI text = victoryText;
            
            if (background != null && text != null)
            {
                // Начальные значения - полностью прозрачные
                Color bgColor = background.color;
                bgColor.a = 0;
                background.color = bgColor;
                
                Color textColor = text.color;
                textColor.a = 0;
                text.color = textColor;
                
                // Анимация появления в течение 1.5 секунд
                float duration = 1.5f;
                float time = 0;
                
                while (time < duration)
                {
                    float bgAlpha = Mathf.Lerp(0, 0.5f, time/duration);
                    bgColor.a = bgAlpha;
                    background.color = bgColor;
                    
                    float textAlpha = Mathf.Lerp(0, 1f, time/duration);
                    textColor.a = textAlpha;
                    text.color = textColor;
                    
                    time += Time.deltaTime;
                    yield return null;
                }
                
                // Финальные значения
                bgColor.a = 0.5f;
                background.color = bgColor;
                textColor.a = 1f;
                text.color = textColor;
            }
        }
    }
}