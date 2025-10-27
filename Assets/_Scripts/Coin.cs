using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Coin Settings")]
    [SerializeField] private float rotationSpeed = 100f;
    
    private bool isCollected = false;
    private Renderer coinRenderer;
    private Collider coinCollider;

    void Start()
    {
        // Получаем компоненты
        coinRenderer = GetComponent<Renderer>();
        coinCollider = GetComponent<Collider>();
        
        if (coinRenderer == null) 
            Debug.LogError("Renderer not found on Coin!");
        if (coinCollider == null) 
            Debug.LogError("Collider not found on Coin!");
    }

    void Update()
    {
        if (!isCollected)
        {
            // Вращение монеты вокруг оси Z
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, что столкнулся игрок и монета еще не собрана
        if (!isCollected && other.CompareTag("Player"))
        {
            CollectCoin();
        }
    }

    void CollectCoin()
    {
        isCollected = true;
        
        // ВОСПРОИЗВОДИМ ЗВУК СБОРА МОНЕТЫ
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayCoinCollectSound();
        }
        else
        {
            Debug.LogWarning("AudioManager not found!");
        }
        
        // Сообщаем GameManager о сборе монеты
        GameManager gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager != null)
        {
            gameManager.AddCoin();
        }
        else
        {
            Debug.LogError("GameManager not found!");
        }
        
        // Отключаем визуальную часть и коллайдер
        if (coinRenderer != null)
            coinRenderer.enabled = false;
        if (coinCollider != null)
            coinCollider.enabled = false;
        
        // Уничтожаем объект через небольшое время
        Destroy(gameObject, 0.5f);
        
        Debug.Log("Coin collected!");
    }
    
    // Статический метод для подсчета монет на сцене
    public static int GetTotalCoinsInScene()
    {
        Coin[] coins = FindObjectsByType<Coin>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        return coins.Length;
    }
}