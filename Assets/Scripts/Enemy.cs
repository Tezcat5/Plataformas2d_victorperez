using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private int healthPoints = 5;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
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
            SoundManager.instance.PlaySFX(SoundManager.instance._monsterAudio);
        } 
    }
}
