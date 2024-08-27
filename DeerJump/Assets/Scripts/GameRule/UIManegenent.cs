using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public partial class GameRuleManegenent : MonoBehaviour
{
    [Header("UIs")]
    [SerializeField] GameObject missUI;
    [SerializeField] GameObject gameoverUI;
    [SerializeField] GameObject goalUI;
    [SerializeField] GameObject allClearUI;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject remainLivesUI;

    [SerializeField] TMP_Text hiScoreText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text stageText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text stageBonusText;
    [SerializeField] TMP_Text timeBonusText;

    void TimerWrite()
    {
        float min = Mathf.Floor(gameTimer / 60);
        float sec = Mathf.Floor(gameTimer % 60);
        float decimals = Mathf.Floor(gameTimer % 1 * 100);

        timerText.text = string.Format("{0} : {1:D2} : {2:D2}", (int)min, (int)sec, (int)decimals);
    }

    void SetUIsFalse()
    {
        missUI.SetActive(false);
        gameoverUI.SetActive(false);
        goalUI.SetActive(false);
        allClearUI.SetActive(false);
    }

    void ChangePauseMode()
    {
        if (pauseUI.activeSelf)
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void RemainLiving()
    {
        var trans = remainLivesUI.GetComponent<RectTransform>();
        trans.sizeDelta = new Vector2(58 * remainLives, trans.sizeDelta.y);
    }
}
