using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _levelCompleteSound;
    [SerializeField] private AudioClip _enemyCollisionSound;
    [SerializeField] private AudioClip _keyCollectSound;
    [SerializeField] private AudioClip _loseSound;
    [SerializeField] private AudioClip _preGameGhostSound;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayPreGameGhostSound());
    }

    private IEnumerator PlayPreGameGhostSound()
    {
        yield return new WaitForSeconds(6.5f);
        _audioSource.PlayOneShot(_preGameGhostSound);
    }

    public void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound);
    }

    public void PLayLelveCompleteSound()
    {
        _audioSource.PlayOneShot(_levelCompleteSound);
    }

    public void PlayEnemyCollision()
    {
        _audioSource.PlayOneShot(_enemyCollisionSound);
    }

    public void PlayKeyCOllectSound()
    {
        _audioSource.PlayOneShot(_keyCollectSound);
    }

    public void PlayLoseSound()
    {
        _audioSource.PlayOneShot(_loseSound);
    }

}
