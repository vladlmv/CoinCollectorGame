using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sound Settings")]
    [SerializeField] private AudioClip coinCollectSound; // Только звук сбора монеты
    
    private AudioSource soundEffectSource;

    void Awake()
    {
        // Реализация паттерна Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Не уничтожать при загрузке новых сцен
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Создаем и настраиваем AudioSource для звуковых эффектов
        soundEffectSource = gameObject.AddComponent<AudioSource>();
        soundEffectSource.playOnAwake = false;
    }

    // Публичный метод для воспроизведения звука сбора монеты
    public void PlayCoinCollectSound()
    {
        if (coinCollectSound != null && soundEffectSource != null)
        {
            soundEffectSource.PlayOneShot(coinCollectSound);
        }
        else
        {
            Debug.LogWarning("Coin collect sound or audio source is not set!");
        }
    }

    // Метод для управления громкостью звуковых эффектов
    public void SetSoundEffectsVolume(float volume)
    {
        if (soundEffectSource != null)
        {
            soundEffectSource.volume = volume;
        }
    }
}