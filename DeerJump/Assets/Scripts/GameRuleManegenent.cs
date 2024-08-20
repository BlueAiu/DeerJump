using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameRuleManegenent : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject water;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            StageReset();
        }
    }

    void StageReset()
    {
        var playerScript = player.GetComponent<PlayerController>();
        playerScript.enabled = true;
        playerScript.Init();

        var waterScript = water.GetComponent<WaterScript>();
        waterScript.enabled = true;
        waterScript.Init();
    }

    public void SendMiss()
    {
        player.GetComponent<PlayerController>().enabled = false;
        water.GetComponent<WaterScript>().enabled = false;

        Invoke(nameof(StageReset), 1f);
    }
}
