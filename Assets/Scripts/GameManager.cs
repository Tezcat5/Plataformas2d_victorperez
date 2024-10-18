using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int coins = 0;
    [SerializeField] Text _coinText;

    private bool isPaused;
    private bool pauseAnimation;
    [SerializeField] GameObject _pauseCanvas;
    private int starsCollected = 0;
    [SerializeField] GameObject[] estrellasActivadas;

    private Animator _pausePanelAnimator;
    [SerializeField]private Slider _healthBar;



    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        _pausePanelAnimator = _pauseCanvas.GetComponentInChildren<Animator>();
    }

    public void Pause()
    {
        if(!isPaused && !pauseAnimation)
        {
            isPaused = true;

            Time.timeScale = 0f;
            _pauseCanvas.SetActive(true);
        }
        else if(isPaused && !pauseAnimation)
        {
            pauseAnimation = true;

            StartCoroutine(ClosePauseAnimation());
        }
    }

    IEnumerator ClosePauseAnimation()
    {
        _pausePanelAnimator.SetBool("Close", true);

        yield return new WaitForSecondsRealtime(0.50f);


        Time.timeScale = 1;
        isPaused = false;
        _pauseCanvas.SetActive(false);

        pauseAnimation = false;
    }

    public void AddCoin()
    {
        coins++;
        _coinText.text = coins.ToString();
        //coins += 1;
    }

    public void AddStar()
    {
        starsCollected++;

        if (starsCollected - 1 < estrellasActivadas.Length)
        {
            estrellasActivadas[starsCollected - 1].SetActive(true);
        }
    }




    public void SetHealthBar(int maxhealth)
    {
        _healthBar.maxValue = maxhealth;
        _healthBar.value = maxhealth;
    }


    public void UpdateHealthBar(int health)
    {
        _healthBar.value = health;
    }
}
