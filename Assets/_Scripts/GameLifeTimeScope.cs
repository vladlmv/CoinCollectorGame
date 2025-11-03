using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameLifetimeScope : LifetimeScope //нейминги под сцены
{
    [Header("Scene References")]
    [SerializeField] private AudioManager audioManager; 
    [SerializeField] private GameManager gameManager;

    protected override void Configure(IContainerBuilder builder)
    {
        // Регистрируем только менеджеры
        builder.RegisterComponent(audioManager);
        builder.RegisterComponent(gameManager);
        
        // НЕ регистрируем монеты - используем простой подход
        builder.RegisterEntryPoint<GameEntryPoint>();
    }
}