using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameEntryPoint : IStartable
{
    private readonly AudioManager _audioManager;
    private readonly GameManager _gameManager;

    public GameEntryPoint(AudioManager audioManager, GameManager gameManager)
    {
        _audioManager = audioManager;
        _gameManager = gameManager;
    }

    void IStartable.Start()
    {
        Debug.Log("VContainer запущен!");
        Debug.Log($"AudioManager: {_audioManager != null}");
        Debug.Log($"GameManager: {_gameManager != null}");
        
        if (_gameManager != null)
        {
            Debug.Log($"Всего монет на уровне: {_gameManager.GetTotalCoins()}");
        }
    }
}