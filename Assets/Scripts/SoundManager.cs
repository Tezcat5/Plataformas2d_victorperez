using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    
    public AudioSource _audioSource;

    public AudioClip _coinAudio;

    public AudioClip _jumpAudio;

    public AudioClip _monsterAudio;

    public AudioClip _deathAudio;



    void Awake()
    {
        if(instance!= null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        _audioSource = GetComponent<AudioSource>();
    }

    /*public void CoinSFX()
    {
        _audioSource.PlayOneShot(_coinAudio);
    }*/

    public void PlaySFX(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

}
