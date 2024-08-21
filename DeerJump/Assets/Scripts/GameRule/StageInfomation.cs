using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    HighJump,
    DoubleJump,
    FastFall
}

public class StageInfomation
{
    public static GameObject platform;
    public static GameObject item;
    public static Sprite[] itemSprites;
    public static GameObject goal;
    public static float gameSizeWidth = 8f;

    const float platformUnit = 0.25f;
    public float platformWidth;
    public List<PlatformScript> platforms = new();
    public List<ItemType> items = new();

    public List<GameObject> CreateStage()
    {
        var stage = new List<GameObject>();

        for (int i = 0;i < platforms.Count; i++)
        {
            Vector3 pos = Vector3.zero;
            pos.y = goal.transform.position.y / (platforms.Count + 1) * (i + 1);

            float width = platformWidth + Random.Range(-platformUnit, platformUnit);
            float posXRange = (gameSizeWidth - width) / 2;

            pos.x = Random.Range(-posXRange, posXRange);

            var _platform = Object.Instantiate(platform, pos, Quaternion.identity);
            _platform.GetComponent<PlatformScript>().Copy(platforms[i], posXRange);

            _platform.GetComponent<BoxCollider2D>().size = new Vector2 (width, platformUnit);
            var renderer = _platform.GetComponent<SpriteRenderer>();
            renderer.size = new Vector2 (width, platformUnit);
            renderer.sprite = PlatformScript.sprites[(int)platforms[i].type];

            stage.Add(_platform);
        }

        foreach (var currentItem in items)
        {
            Vector3 pos = Vector3.zero;
            float xRange = gameSizeWidth / 2 - 0.5f;
            pos.x = Random.Range(-xRange, xRange);
            pos.y = Random.Range(1, goal.transform.position.y - 3);

            var _item = Object.Instantiate(item, pos, Quaternion.identity);
            _item.name += " " + currentItem;
            _item.GetComponent<SpriteRenderer>().sprite = itemSprites[(int)currentItem];

            stage.Add(_item);
        }

        return stage;
    }
}