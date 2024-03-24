using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    public Text countdownText;
    public static bool GameHasStarted = false;

    void Start()
    {
        Time.timeScale = 0;
        
        StartCoroutine(StartCountdown());
        
        GameHasStarted = false;

    }

    IEnumerator StartCountdown()
    {
        float countdown = 3;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdown--;
        }

        Time.timeScale = 1;
        countdownText.gameObject.SetActive(false);

        GameHasStarted = true;
    }

}