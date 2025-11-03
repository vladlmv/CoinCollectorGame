using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private int totalCoinsInLevel = 10;
    
    private int collectedCoins = 0;
    private bool isGameActive = true;
    
    public System.Action<int> OnCoinCollected;
    public System.Action OnGameWon;
    
    public bool IsGameActive 
    { 
        get { return isGameActive; } 
        private set { isGameActive = value; } 
    }
    
    void Start()
    {
        // Автоматически подсчитываем монеты на сцене
        totalCoinsInLevel = Coin.GetTotalCoinsInScene();
        Debug.Log($"Game started. Total coins: {totalCoinsInLevel}");
    }
    
    public void AddCoin()
    {
        collectedCoins++;
        Debug.Log($"Coin collected! Total: {collectedCoins}/{totalCoinsInLevel}");
        
        OnCoinCollected?.Invoke(collectedCoins);
        
        if (collectedCoins >= totalCoinsInLevel)
        {
            WinGame();
        }
    }
    
    void WinGame()
    {
        isGameActive = false;
        Debug.Log("ПОБЕДА! Все монеты собраны!");
        
        OnGameWon?.Invoke();
    }
    
    public int GetTotalCoins()
    {
        return totalCoinsInLevel;
    }
}