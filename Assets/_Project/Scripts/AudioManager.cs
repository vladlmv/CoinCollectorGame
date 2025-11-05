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
    }

    public void PlayCoinCollectSound()
    {
        if (coinCollectSound != null && soundEffectSource != null)
        {
            soundEffectSource.PlayOneShot(coinCollectSound);
        }
        else
        {
            Debug.LogError($"AudioManager: проблема с soundEffectSource или coinCollectSound");
        }
    }
}