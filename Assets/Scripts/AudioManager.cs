using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip startMusic;
    public AudioClip levelZeroMusic;
    public AudioClip levelOneMusic;
    public AudioClip gameOverMusic;

    public void PlayStartMusic() => PlayClip(startMusic);
    public void PlayLevelZeroMusic() => PlayClip(levelZeroMusic);
    public void PlayLevelOneMusic() => PlayClip(levelOneMusic);
    public void PlayGameOverMusic() => PlayClip(gameOverMusic);

    private void PlayClip(AudioClip clip)
    {
        if (audioSource.clip == clip) return;
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
