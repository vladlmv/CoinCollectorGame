using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Coin Settings")]
    [SerializeField] private float rotationSpeed = 100f;
    
    private bool isCollected = false;
    private Renderer coinRenderer;
    private Collider coinCollider;
    private AudioManager _audioManager;
    private GameManager _gameManager;

    void Start()
    {
        coinRenderer = GetComponent<Renderer>();
        coinCollider = GetComponent<Collider>();
        
        _audioManager = FindAnyObjectByType<AudioManager>();
        _gameManager = FindAnyObjectByType<GameManager>();
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
        
        if (_audioManager != null)
        {
            _audioManager.PlayCoinCollectSound();
        }
        else
        {
            Debug.LogError("AudioManager не найден!");
        }
        
        if (_gameManager != null)
        {
            _gameManager.AddCoin();
        }
        else
        {
            Debug.LogError("GameManager не найден!");
        }
        
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