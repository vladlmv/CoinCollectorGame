using _Project.Scripts.MVP;
using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameEntryPoint : IStartable
{
    private readonly AudioManager _audioManager;
    private readonly GameManager _gameManager;
    private readonly IGamePresenter _gamePresenter;

    public GameEntryPoint(
        AudioManager audioManager, 
        GameManager gameManager,
        IGamePresenter gamePresenter)
    {
        _audioManager = audioManager;
        _gameManager = gameManager;
        _gamePresenter = gamePresenter;
    }

    void IStartable.Start()
    {
    }
}