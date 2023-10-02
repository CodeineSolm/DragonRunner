using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSoundEffects : MonoBehaviour
{
    [SerializeField] private AudioClip _jumpAudioClip;
    [SerializeField] private AudioClip _shootAudioClip;
    [SerializeField] private AudioClip _hurtAudioClip;
    [SerializeField] private AudioClip _deathAudioClip;

    private AudioSource _audioSource;

    public void Jump()
    {
        _audioSource.clip = _jumpAudioClip;
        _audioSource.Play();
    }

    public void Shoot()
    {
        _audioSource.clip = _shootAudioClip;
        _audioSource.Play();
    }

    public void Hurt()
    {
        _audioSource.clip = _hurtAudioClip;
        _audioSource.Play();
    }

    public void Death()
    {
        _audioSource.clip = _deathAudioClip;
        _audioSource.Play();
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
