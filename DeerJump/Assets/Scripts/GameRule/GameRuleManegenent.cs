using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class GameRuleManegenent : MonoBehaviour
{
    public static bool isGameDoing = true;

    [SerializeField] GameObject player;
    [SerializeField] GameObject water;

    [SerializeField] GameObject platformPrefab;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Sprite[] itemSprites;
    [SerializeField] GameObject goal;
    [SerializeField] float gameSizeWidth = 8f;

    [SerializeField] Sprite[] platformSsprites;

    [SerializeField] int stageNum = 0;
    public StageInfomation[] Stages { get; set; } = new StageInfomation[25];
    public List<GameObject> StagePlacement { get; set; } = new();


    // Start is called before the first frame update
    void Start()
    {
        StageInfomation.platform = platformPrefab;
        StageInfomation.item = itemPrefab;
        StageInfomation.itemSprites = itemSprites;
        StageInfomation.goal = goal;
        StageInfomation.gameSizeWidth = gameSizeWidth;

        PlatformScript.sprites = platformSsprites;

        SetStageDatas();
        StagePlacement = Stages[stageNum].CreateStage();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        foreach (var item in StagePlacement)
        {
            item.SetActive(true);
        }
    }

    public void SendMiss()
    {
        isGameDoing = false;
        player.GetComponent<PlayerController>().enabled = false;
        water.GetComponent<WaterScript>().enabled = false;

        Invoke(nameof(StageReset), 1f);
    }
}
