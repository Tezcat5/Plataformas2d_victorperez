using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private int healthPoints = 5;
    
    private AudioSource _audioSource;
    
    // Start is called before the first frame update



    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    void Start()
    {
        SoundManager.instance.PlaySFX(_audioSource, SoundManager.instance._mimikAudio);
    }

    // Update is called once per frame
    
    
    
    
    
    
    
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        healthPoints -= 1;

        if(healthPoints <= 0)
        {
            Destroy(gameObject);
            SoundManager.instance.PlaySFX(_audioSource, SoundManager.instance._mimikAudio);
        } 
    }
}
