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
    [SerializeField] GameObject _pauseCanvas;

    private Animator _pausePanelAnimator;



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
        if(!isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
            _pauseCanvas.SetActive(true);
        }
        else
        {
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
    }

    public void AddCoin()
    {
        coins++;
        _coinText.text = coins.ToString();
        //coins += 1;
    }
}
