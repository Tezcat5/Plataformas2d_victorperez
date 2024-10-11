using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    
    public AudioSource _audioSource;

    public AudioClip _coinAudio;

    public AudioClip _jumpAudio;

    public AudioClip _mimikAudio;

    public AudioClip _deathAudio;

    public AudioClip[] _audios;



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

    public void PlaySFX(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

}
