using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    public Text countdownText; // Přiřaďte tento objekt v Unity Editoru

    void Start()
    {
        Time.timeScale = 0; // Pozastaví všechny akce ve hře
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        float countdown = 3; // Nastavte počáteční čas odpočtu
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSecondsRealtime(1f); // Použije reálný čas, i když je Time.timeScale na 0
            countdown--;
        }

        //countdownText.text = "Go!";
        Time.timeScale = 1; // Obnoví normální chod hry
        //yield return new WaitForSecondsRealtime(1f); // Opět použije reálný čas pro zpoždění skrytí textu
        countdownText.gameObject.SetActive(false); // Skryje časovač
    }
}