using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.MVP
{
    public interface IGamePresenter : IInitializable, ITickable
    {
    }
    
    public class GamePresenter : IGamePresenter, System.IDisposable
    {
        private readonly IGameModel _model;
        private readonly GamePresenterConfig _config;
        private GameView _view;

        [Inject]
        public GamePresenter(IGameModel model, GamePresenterConfig config)
        {
            _model = model;
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
            
            // Подписываемся на события Model
            _model.CoinCollected += OnCoinCollected;
            _model.GameWon += OnGameWon;
            _model.TimerUpdated += OnTimerUpdated;
            
            // Инициализируем начальное состояние
            UpdateCoinDisplay();
        }

        void ITickable.Tick()
        {
            // Обновляем таймер каждый кадр
            _model.UpdateTimer(Time.deltaTime);
        }

        public void Dispose()
        {
            // Отписываемся от событий
            if (_view != null)
            {
                _view.RestartButtonClicked -= OnRestartButtonClicked;
                _view.MenuButtonClicked -= OnMenuButtonClicked;
            }
            
            _model.CoinCollected -= OnCoinCollected;
            _model.GameWon -= OnGameWon;
            _model.TimerUpdated -= OnTimerUpdated;
        }

        // Обработчики событий от View
        private void OnRestartButtonClicked()
        {
            _model.RestartGame();
        }

        private void OnMenuButtonClicked()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

        // Обработчики событий от Model
        private void OnCoinCollected(int collectedCoins)
        {
            UpdateCoinDisplay();
        }

        private void OnGameWon()
        {
            _view.ShowVictoryScreen(_model.GameTime);
        }

        private void OnTimerUpdated(float gameTime)
        {
            _view.UpdateTimer(gameTime);
        }

        private void UpdateCoinDisplay()
        {
            _view.UpdateCoinCounter(_model.CollectedCoins, _model.TotalCoins);
        }
    }
}