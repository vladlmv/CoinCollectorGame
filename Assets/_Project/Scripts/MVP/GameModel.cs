using System;
using _Project.Scripts.Services;

namespace _Project.Scripts.MVP
{
    public interface IGameModel
    {
        event Action GameWon;
        bool IsGameActive { get; }
        void Initialize();
        void NotifyGameWon();
    }

    public class GameModel : IGameModel
    {
        public event Action GameWon;
        public bool IsGameActive { get; private set; } = true;

        private readonly IBankService _bankService;

        public GameModel(IBankService bankService)
        {
            _bankService = bankService;
            
            // Подписываемся на событие сбора всех монет
            _bankService.OnCoinsChanged += OnCoinsChanged;
        }

        public void Initialize()
        {
            IsGameActive = true;
        }

        public void NotifyGameWon()
        {
            IsGameActive = false;
            GameWon?.Invoke();
        }

        private void OnCoinsChanged(int coins)
        {
            if (_bankService.IsAllCoinsCollected && IsGameActive)
            {
                NotifyGameWon();
            }
        }
    }
}