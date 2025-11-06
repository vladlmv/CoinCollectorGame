using System;

namespace _Project.Scripts.Services
{
    public interface IBankService
    {
        event Action<int> OnCoinsChanged;
        event Action<int> OnCoinCollected; 
        event Action OnBankInitialized;
        int CurrentCoins { get; }
        int TotalCoinsInLevel { get; }
        
        void CollectCoin();
        void Initialize(int totalCoins);
        void Reset();
        bool IsAllCoinsCollected { get; }
    }

    public class BankService : IBankService
    {
        public event Action<int> OnCoinsChanged;
        public event Action<int> OnCoinCollected;
        public event Action OnBankInitialized;

        public int CurrentCoins { get; private set; }
        public int TotalCoinsInLevel { get; private set; }
        public bool IsAllCoinsCollected => CurrentCoins >= TotalCoinsInLevel;

        public void CollectCoin()
        {
            if (IsAllCoinsCollected) return;
            
            CurrentCoins++;
            
            OnCoinCollected?.Invoke(CurrentCoins);
            OnCoinsChanged?.Invoke(CurrentCoins);
        }

        public void Initialize(int totalCoins)
        {
            TotalCoinsInLevel = totalCoins;
            CurrentCoins = 0;
            
            OnBankInitialized?.Invoke();
        }

        public void Reset()
        {
            CurrentCoins = 0;
            
            OnCoinsChanged?.Invoke(CurrentCoins);
        }
    }
}