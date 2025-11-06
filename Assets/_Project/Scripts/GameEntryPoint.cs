using _Project.Scripts.MVP;
using VContainer;
using VContainer.Unity;
using UnityEngine;
using _Project.Scripts.Services;

public class GameEntryPoint : IStartable
{
    private readonly AudioManager _audioManager;
    private readonly GameManager _gameManager;
    private readonly IGamePresenter _gamePresenter;
    private readonly IBankService _bankService;

    public GameEntryPoint(
        AudioManager audioManager, 
        GameManager gameManager,
        IGamePresenter gamePresenter,
        IBankService bankService)
    {
        _audioManager = audioManager;
        _gameManager = gameManager;
        _gamePresenter = gamePresenter;
        _bankService = bankService;
    }

    void IStartable.Start()
    {
    }
}