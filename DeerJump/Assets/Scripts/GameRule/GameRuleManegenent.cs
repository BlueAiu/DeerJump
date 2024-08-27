using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class GameRuleManegenent : MonoBehaviour
{
    public static bool isGameDoing = true;

    [SerializeField] GameObject player;
    [SerializeField] GameObject water;
    [SerializeField] new Transform camera;

    [SerializeField] GameObject platformPrefab;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Sprite[] itemSprites;
    [SerializeField] GameObject goal;
    [SerializeField] float gameSizeWidth = 8f;

    [SerializeField] Sprite[] platformSsprites;

    [SerializeField] int stageNum = 0;
    public StageInfomation[] Stages { get; set; } = new StageInfomation[25];
    public List<GameObject> StagePlacement { get; set; } = new();

    public float gameTimer;

    new AudioSource audio;
    [SerializeField] AudioClip allClear;
    [SerializeField] AudioClip gameover;

    [SerializeField] float goalTime = 1.5f;
    [SerializeField] float allClearTime = 2f;
    [SerializeField] float missTime = 1f;

    [SerializeField] int remainLives;
    bool isGameOvering = false;


    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();

        StageInfomation.platform = platformPrefab;
        StageInfomation.item = itemPrefab;
        StageInfomation.itemSprites = itemSprites;
        StageInfomation.goal = goal;
        StageInfomation.gameSizeWidth = gameSizeWidth;

        PlatformScript.sprites = platformSsprites;

        if (PlayerPrefs.HasKey("HighScore"))
        {
            HighScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            HighScore = 0;
        }
        scoreText.text = Score.ToString();
        hiScoreText.text = HighScore.ToString();

        SetStageDatas();
        StagePlacement = Stages[stageNum].CreateStage();

        ChangePauseMode();

        remainLives = 3;
        RemainLiving();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameDoing)
            gameTimer += Time.deltaTime;
        TimerWrite();

        if (InputManeger.IsPauseButton())
            ChangePauseMode();

        Continuing();
    }

    void StageReset()
    {
        isGameDoing = true;

        var playerScript = player.GetComponent<PlayerController>();
        playerScript.enabled = true;
        playerScript.Init();

        var waterScript = water.GetComponent<WaterScript>();
        waterScript.enabled = true;
        waterScript.Init();

        camera.position = new Vector3(1, 2, -10);

        foreach (var item in StagePlacement)
        {
            item.SetActive(true);
        }

        gameTimer = 0;

        SetUIsFalse();
        stageText.text = "STAGE " + (stageNum + 1).ToString().PadLeft(2);
    }

    void NextStage()
    {
        stageNum++;
        foreach(var item in StagePlacement)
        {
            Destroy(item);
        }

        StagePlacement = Stages[stageNum].CreateStage();
        StageReset();
    }

    public void SendMiss()
    {
        isGameDoing = false;
        player.GetComponent<PlayerController>().enabled = false;
        water.GetComponent<WaterScript>().enabled = false;

        missUI.SetActive(true);

        remainLives--;
        RemainLiving();

        if (remainLives == 0)
        {
            Invoke(nameof(GameOver), missTime);
        }
        else
        {
            Invoke(nameof(StageReset), missTime);
        }
    }

    public void SendClear()
    {
        isGameDoing = false;
        player.GetComponent<PlayerController>().enabled = false;
        water.GetComponent<WaterScript>().enabled = false;

        goalUI.SetActive(true);
        Scoring();

        if (stageNum == Stages.Length - 1)
        {
            Invoke(nameof(AllClear), goalTime);
        }
        else
        {
            Invoke(nameof(NextStage), goalTime);
        }
    }

    void GameOver()
    {
        missUI.SetActive(false);
        gameoverUI.SetActive(true);

        audio.clip = gameover;
        audio.Play();

        isGameOvering = true;
    }

    void AllClear()
    {
        goalUI.SetActive(false);
        allClearUI.SetActive(true);

        audio.clip = allClear;
        audio.Play();

        stageNum = 0 - 1;
        Invoke(nameof(NextStage), 1f);
    }

    void Continuing()
    {
        if (!isGameOvering) return;
        if (pauseUI.activeSelf) return;

        if (InputManeger.IsDecision())
        {
            Score = 0;
            remainLives = 3;
            stageNum = Mathf.FloorToInt(stageNum / 10) * 10 - 1;
            NextStage();
        }
        else if(InputManeger.IsCanceled())
        {
            Score = 0;
            remainLives = 3;
            stageNum = 0 - 1;
            NextStage();
        }
    }
}
