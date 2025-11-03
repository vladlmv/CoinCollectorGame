using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Elements")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    
    void Start()
    {
        // Назначаем обработчики кнопок
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
        }
        
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
        
    }
    
    // Запуск игры
    void StartGame()
    {
        Debug.Log("Запуск игры...");
        
        SceneManager.LoadScene("SampleScene");
    }
    
    // Выход из игры
    void QuitGame()
    {
        Debug.Log("Выход из игры...");
        
        // Выход из приложения
        Application.Quit();
        
        // Для тестирования в редакторе Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}