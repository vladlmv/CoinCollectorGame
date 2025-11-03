using _Project.Scripts.MVP;
using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameScope : LifetimeScope //нейминги под сцены
{
    [Header("Scene References")]
    [SerializeField] private AudioManager audioManager; 
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GamePresenterConfig _gamePresenterConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        // Регистрируем только менеджеры
        builder.RegisterComponent(audioManager);
        builder.RegisterComponent(gameManager);
        
        // НЕ регистрируем монеты - используем простой подход
        builder.RegisterEntryPoint<GameEntryPoint>();
        
        builder.Register<GameModel>(Lifetime.Scoped)
            .As<IGameModel>();
        
        builder.RegisterEntryPoint<GamePresenter>(Lifetime.Scoped)
            .As<IGamePresenter>() 
            .WithParameter(_gamePresenterConfig);
    }
}