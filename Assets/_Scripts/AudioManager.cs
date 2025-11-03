using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] private AudioClip coinCollectSound;
    private AudioSource soundEffectSource;

    void Start()
    {
        soundEffectSource = gameObject.AddComponent<AudioSource>();
        soundEffectSource.playOnAwake = false;
        Debug.Log("AudioManager инициализирован");
    }

    public void PlayCoinCollectSound()
    {
        if (coinCollectSound != null && soundEffectSource != null)
        {
            soundEffectSource.PlayOneShot(coinCollectSound);
            Debug.Log("AudioManager: звук воспроизведен!");
        }
        else
        {
            Debug.LogError($"AudioManager: проблема с soundEffectSource или coinCollectSound");
        }
    }
}