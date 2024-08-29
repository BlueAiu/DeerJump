using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using unityroom.Api;

public partial class GameRuleManegenent : MonoBehaviour
{
    static public int Score = 0;
    static public int HighScore = 0;
    int liveScore = 0;
    const int liveBonus = 10000;
    [Header("Score")]
    [SerializeField] int[] clearScore = new int[3];
    [SerializeField] int[] timeScoreBase = new int[3];
    [SerializeField] float timeLimit = 40f;

    int ScoreNum()
    {
        return Mathf.FloorToInt(stageNum / 10f);
    }

    int TimeScore()
    {
        float timeLate = Mathf.Min(gameTimer / timeLimit, 1f);
        return (int)(timeScoreBase[ScoreNum()] * (1 - timeLate));
    }

    void Scoring()
    {
        int _clearScore = clearScore[ScoreNum()];
        int _timeScore = TimeScore();

        stageBonusText.text = "STAGE BONUS  " + _clearScore.ToString().PadLeft(4);
        timeBonusText.text = "TIME    BONUS  " + _timeScore.ToString().PadLeft(4);

        Score += _clearScore + _timeScore;
        scoreText.text = Score.ToString();

        liveScore += _clearScore + _timeScore;
        if(liveScore >= liveBonus)
        {
            remainLives++;
            liveScore -= liveBonus;
        }

        HighScore = Mathf.Max(HighScore, Score);
        PlayerPrefs.SetInt("HighScore", HighScore);
        hiScoreText.text = HighScore.ToString();

        UnityroomApiClient.Instance.SendScore(1, Score, ScoreboardWriteMode.HighScoreDesc);
    }
}
