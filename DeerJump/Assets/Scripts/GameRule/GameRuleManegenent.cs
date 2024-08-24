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


    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameDoing)
            gameTimer += Time.deltaTime;
        TimerWrite();

        if (InputManeger.IsPauseButton())
            ChangePauseMode();
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

        Invoke(nameof(StageReset), 1f);
    }

    public void SendClear()
    {
        isGameDoing = false;
        player.GetComponent<PlayerController>().enabled = false;
        water.GetComponent<WaterScript>().enabled = false;

        goalUI.SetActive(true);
        Scoring();

        Invoke(nameof(NextStage), 1f);
    }
}
