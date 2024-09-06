using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    Camera cam;

    float targetSize = 3.5f;
    [SerializeField] float normalSize = 3.5f;
    [SerializeField] float wideSize = 5f;
    [SerializeField] float changeTime = 0.5f;
    float changeSpeed;

    [SerializeField] GameObject[] blackSides;

    void Awake()
    {
        cam = GetComponent<Camera>();
        changeSpeed = (wideSize - normalSize) / changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, changeSpeed * Time.deltaTime);
    }

    public bool IsSizeWide
    {
        set
        {
            if (value == true)
            {
                targetSize = wideSize;

                foreach(var obj in blackSides)
                {
                    obj.SetActive(true);
                }
            }
            else
            {
                targetSize = normalSize;

                foreach (var obj in blackSides)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    public void SetNormalFast()
    {
        if(cam == null)
        {
            cam = GetComponent<Camera>();
        }
        cam.orthographicSize = normalSize;

        foreach (var obj in blackSides)
        {
            obj.SetActive(false);
        }
    }
}
