using UnityEngine;
using _Project.Scripts.Services;
using VContainer;

public class Coin : MonoBehaviour
{
    [Header("Coin Settings")]
    [SerializeField] private float rotationSpeed = 100f;
    
    private bool isCollected = false;
    private Renderer coinRenderer;
    private Collider coinCollider;
    private IBankService _bankService;
    private AudioManager _audioManager;

    void Start()
    {
        coinRenderer = GetComponent<Renderer>();
        coinCollider = GetComponent<Collider>();
        
        _bankService = FindAnyObjectByType<GameScope>()?.Container?.Resolve<IBankService>();
        _audioManager = FindAnyObjectByType<AudioManager>();
    }

    void Update()
    {
        if (!isCollected)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isCollected && other.CompareTag("Player"))
        {
            CollectCoin();
        }
    }

    void CollectCoin()
    {
        isCollected = true;
        
        // Логика сбора монеты
        if (_bankService != null)
        {
            _bankService.CollectCoin();
        }
        
        // Воспроизводим звук НЕПОСРЕДСТВЕННО в монете
        if (_audioManager != null)
        {
            _audioManager.PlayCoinCollectSound();
        }
        else
        {
            Debug.LogError("Coin: AudioManager не найден!");
        }
        
        // Визуальное скрытие монеты
        if (coinRenderer != null)
            coinRenderer.enabled = false;
        if (coinCollider != null)
            coinCollider.enabled = false;
        
        Destroy(gameObject, 0.5f);
    }
    
    public static int GetTotalCoinsInScene()
    {
        Coin[] coins = FindObjectsByType<Coin>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        return coins.Length;
    }
}