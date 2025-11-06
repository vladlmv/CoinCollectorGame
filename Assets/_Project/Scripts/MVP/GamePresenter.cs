using UnityEngine;
using VContainer;
using VContainer.Unity;
using _Project.Scripts.Services;

namespace _Project.Scripts.MVP
{
    public interface IGamePresenter : IInitializable, ITickable
    {
    }
    
    public class GamePresenter : IGamePresenter, System.IDisposable
    {
        private readonly IBankService _bankService; // Добавил BankService
        private readonly GamePresenterConfig _config;
        private GameView _view;
        private float _gameTime;

        [Inject]
        public GamePresenter(IBankService bankService, GamePresenterConfig config, AudioManager audioManager)
        {
            _bankService = bankService; // Получаем BankService через DI
            _config = config;
        }

        void IInitializable.Initialize()
        {
            // Создаем View из префаба
            _view = Object.Instantiate(_config.GameView, null);
            _view.Initialize();
            
            // Подписываемся на события View
            _view.RestartButtonClicked += OnRestartButtonClicked;
            _view.MenuButtonClicked += OnMenuButtonClicked;
            
            // Подписываемся на события BankService вместо GameManager
            _bankService.OnCoinCollected += OnCoinCollected;
            _bankService.OnCoinsChanged += OnCoinsChanged;
            _bankService.OnBankInitialized += OnBankInitialized;
            
            InitializeGame();
        }
        

        void ITickable.Tick()
        {
            _gameTime += Time.deltaTime;
            _view.UpdateTimer(_gameTime);
        }

        public void Dispose()
        {
            // Отписываемся от событий
            if (_view != null)
            {
                _view.RestartButtonClicked -= OnRestartButtonClicked;
                _view.MenuButtonClicked -= OnMenuButtonClicked;
            }
            
            _bankService.OnCoinCollected -= OnCoinCollected;
            _bankService.OnCoinsChanged -= OnCoinsChanged;
            _bankService.OnBankInitialized -= OnBankInitialized;
        }

        // Обработчики событий от View
        private void OnRestartButtonClicked()
        {
            RestartGame();
        }

        private void OnMenuButtonClicked()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

        // Обработчики событий от BankService
        private void OnCoinCollected(int collectedCoins)
        {
            UpdateCoinDisplay();
            
            if (_bankService.IsAllCoinsCollected)
            {
                OnGameWon();
            }
        }

        private void OnCoinsChanged(int coins)
        {
            // Дополнительная логика при изменении количества монет
            UpdateCoinDisplay();
        }

        private void OnBankInitialized()
        {
            UpdateCoinDisplay();
        }

        private void OnGameWon()
        {
            _view.ShowVictoryScreen(_gameTime);
        }

        private void InitializeGame()
        {
            _gameTime = 0f;
            UpdateCoinDisplay();
            _view.UpdateTimer(_gameTime);
        }

        private void UpdateCoinDisplay()
        {
            _view.UpdateCoinCounter(_bankService.CurrentCoins, _bankService.TotalCoinsInLevel);
        }

        private void RestartGame()
        {
            _bankService.Reset();
            _gameTime = 0f;
            InitializeGame();
        }
    }
}