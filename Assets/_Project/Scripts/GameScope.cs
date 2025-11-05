using _Project.Scripts.MVP;
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
        
        // Регистрируем MVP компоненты
        builder.Register<IGameModel, GameModel>(Lifetime.Scoped)
            .WithParameter(gameManager); // Передаем GameManager в конструктор
        
        builder.RegisterInstance(_gamePresenterConfig);
        
        builder.RegisterEntryPoint<GamePresenter>(Lifetime.Scoped)
            .As<IGamePresenter>();
        
        builder.RegisterEntryPoint<GameEntryPoint>();
    }
}