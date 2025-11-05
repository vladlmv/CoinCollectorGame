using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI coinCounterText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject victoryImage;
    [SerializeField] private TextMeshProUGUI victoryText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;
    
    private GameManager gameManager;
    private float gameTime = 0f;
    private bool isTimerRunning = true;
    
    void Start()
    {
        // Выключаем VictoryImage и кнопки при старте
        InitializeUI();
        
        // Находим GameManager и подписываемся на события
        FindAndSubscribeToGameManager();
        
        // Инициализируем и запускаем таймер
        InitializeTimer();
    }
    
    void InitializeUI()
    {
        // Выключаем панель победы
        if (victoryImage != null)
        {
            victoryImage.SetActive(false);
        }
        
        // Выключаем и настраиваем кнопки
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(false);
            restartButton.onClick.AddListener(RestartGame);
        }
        
        if (menuButton != null)
        {
            menuButton.gameObject.SetActive(false);
            menuButton.onClick.AddListener(GoToMainMenu);
        }
    }
    
    void FindAndSubscribeToGameManager()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager != null)
        {
            // Подписываемся на события GameManager
            gameManager.OnCoinCollected += OnCoinCollected;
            gameManager.OnGameWon += OnGameWon;
        }
    }
    
    void InitializeTimer()
    {
        // Устанавливаем начальное значение таймера
        if (timerText != null)
        {
            timerText.text = "00:00";
        }
        
        // Запускаем корутину обновления таймера
        StartCoroutine(UpdateTimerCoroutine());
    }
    
    // Корутина для обновления таймера каждый кадр
    IEnumerator UpdateTimerCoroutine()
    {
        while (isTimerRunning)
        {
            if (gameManager != null && gameManager.IsGameActive)
            {
                gameTime += Time.deltaTime;
                UpdateTimerDisplay();
            }
            yield return null; // Ждем следующий кадр
        }
    }
    
    // Обновление отображения таймера в формате MM:SS
    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(gameTime / 60);
            int seconds = Mathf.FloorToInt(gameTime % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
    
    // Обновление счетчика монет
    public void UpdateCoinCount(int collected, int total)
    {
        if (coinCounterText != null)
        {
            coinCounterText.text = $"Монеты: <color=#FFD700>{collected}</color>/{total}";
        }
    }
    
    // Обработчик события сбора монеты
    void OnCoinCollected(int collectedCoins)
    {
        UpdateCoinCount(collectedCoins, gameManager.GetTotalCoins());
    }
    
    // Обработчик события победы
    void OnGameWon()
    {
        ShowWinMessage();
    }
    
    // Показать сообщение о победе
    public void ShowWinMessage()
    {
        // Останавливаем таймер
        isTimerRunning = false;
        
        if (victoryImage != null)
        {
            // Включаем панель победы
            victoryImage.SetActive(true);
            
            // Обновляем текст с временем
            if (victoryText != null)
            {
                int minutes = Mathf.FloorToInt(gameTime / 60);
                int seconds = Mathf.FloorToInt(gameTime % 60);
                victoryText.text = $"ПОБЕДА!\n<size=24>Время: {minutes:00}:{seconds:00}</size>";
            }
            
            // Показываем кнопки управления
            if (restartButton != null) 
                restartButton.gameObject.SetActive(true);
            if (menuButton != null) 
                menuButton.gameObject.SetActive(true);
            
            // Запускаем анимацию плавного появления
            StartCoroutine(FadeInVictoryMessage());
        }
    }
    
    // Анимация плавного появления панели победы
    IEnumerator FadeInVictoryMessage()
    {
        // Получаем компоненты для анимации
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
                // Плавное увеличение прозрачности фона (до 50%)
                float bgAlpha = Mathf.Lerp(0, 0.5f, time/duration);
                bgColor.a = bgAlpha;
                background.color = bgColor;
                
                // Плавное увеличение прозрачности текста (до 100%)
                float textAlpha = Mathf.Lerp(0, 1f, time/duration);
                textColor.a = textAlpha;
                text.color = textColor;
                
                time += Time.deltaTime;
                yield return null; // Ждем следующий кадр
            }
            
            // Финальные значения
            bgColor.a = 0.5f;
            background.color = bgColor;
            textColor.a = 1f;
            text.color = textColor;
        }
    }
    
    // Перезапуск текущей сцены
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    // Возврат в главное меню
    void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Убедитесь, что это имя вашей сцены меню
    }
    
    // Отписываемся от событий при уничтожении объекта
    void OnDestroy()
    {
        if (gameManager != null)
        {
            gameManager.OnCoinCollected -= OnCoinCollected;
            gameManager.OnGameWon -= OnGameWon;
        }
    }
}