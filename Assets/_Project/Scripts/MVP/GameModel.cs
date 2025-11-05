using System;

namespace _Project.Scripts.MVP
{
    public interface IGameModel
    {
        event Action<int> CoinCollected;
        event Action GameWon;
        event Action<float> TimerUpdated;
        
        int CollectedCoins { get; }
        int TotalCoins { get; }
        bool IsGameActive { get; }
        float GameTime { get; }
        
        void CollectCoin();
        void UpdateTimer(float deltaTime);
        void RestartGame();
    }

    public class GameModel : IGameModel
    {
        public event Action<int> CoinCollected;
        public event Action GameWon;
        public event Action<float> TimerUpdated;

        public int CollectedCoins { get; private set; }
        public int TotalCoins { get; private set; }
        public bool IsGameActive { get; private set; } = true;
        public float GameTime { get; private set; }

        private readonly GameManager _gameManager;

        public GameModel(GameManager gameManager)
        {
            _gameManager = gameManager;
            TotalCoins = _gameManager.GetTotalCoins();
            
            // Подписываемся на события GameManager
            _gameManager.OnCoinCollected += OnGameManagerCoinCollected;
            _gameManager.OnGameWon += OnGameManagerGameWon;
        }

        public void CollectCoin()
        {
            if (!IsGameActive) return;
            
            _gameManager.AddCoin();
        }

        public void UpdateTimer(float deltaTime)
        {
            if (!IsGameActive) return;
            
            GameTime += deltaTime;
            TimerUpdated?.Invoke(GameTime);
        }

        public void RestartGame()
        {
            // Перезагрузка сцены через SceneManager
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        private void OnGameManagerCoinCollected(int collectedCoins)
        {
            CollectedCoins = collectedCoins;
            CoinCollected?.Invoke(collectedCoins);

            if (collectedCoins >= TotalCoins)
            {
                IsGameActive = false;
            }
        }

        private void OnGameManagerGameWon()
        {
            IsGameActive = false;
            GameWon?.Invoke();
        }
    }
}