using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AudioType
{
    Jump,
    ItemGet,
    HighJump,
    Clear,
    Miss,
    Gameover
}

public partial class PlayerController : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioClip[] audioClips;

    new AudioSource audio;

    void PlaySE(AudioType type)
    {
        audio.clip = audioClips[(int)type];
        audio.Play();
    }
}
