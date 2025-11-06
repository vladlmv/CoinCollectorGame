using _Project.Scripts.MVP;
using _Project.Scripts.Services;
using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameScope : LifetimeScope
{
    [Header("Scene References")]
    [SerializeField] private AudioManager audioManager; 
    [SerializeField] private GameManager gameManager;
    
    [Header("MVP Configuration")]
    [SerializeField] private GamePresenterConfig _gamePresenterConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        // Регистрируем менеджеры
        builder.RegisterComponent(audioManager);
        builder.RegisterComponent(gameManager);
        
        // Регистрируем BankService
        builder.Register<IBankService, BankService>(Lifetime.Singleton);
        
        // Регистрируем MVP компоненты
        builder.Register<IGameModel, GameModel>(Lifetime.Scoped);
        
        builder.RegisterInstance(_gamePresenterConfig);
        
        builder.RegisterEntryPoint<GamePresenter>(Lifetime.Scoped)
            .As<IGamePresenter>();
        
        builder.RegisterEntryPoint<GameEntryPoint>();
        
        builder.RegisterBuildCallback(container =>
        {
            var bankService = container.Resolve<IBankService>();
            var totalCoins = Coin.GetTotalCoinsInScene();
            bankService.Initialize(totalCoins);
        });
    }
}