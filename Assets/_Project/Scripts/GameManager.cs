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
    }
    
    public void AddCoin()
    {
        collectedCoins++;
        
        OnCoinCollected?.Invoke(collectedCoins);
        
        if (collectedCoins >= totalCoinsInLevel)
        {
            WinGame();
        }
    }
    
    void WinGame()
    {
        isGameActive = false;
        
        OnGameWon?.Invoke();
    }
    
    public int GetTotalCoins()
    {
        return totalCoinsInLevel;
    }
}