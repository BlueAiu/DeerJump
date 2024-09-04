using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    [Header("k‚Þg‘Ì‚ÆF‚Ì•Ï‰»")]
    [Tooltip("‚Ç‚±‚Ü‚Åk‚Þ‚©")]
    [SerializeField] float shrinkSize = 0.5f;

    [SerializeField] SpriteRenderer[] renderers;
    [SerializeField] float ColorChangePeriod = 0.2f;

    [SerializeField] GameObject whiteDeer;
    [SerializeField] GameObject face;
    [SerializeField] GameObject horn;
    [SerializeField] GameObject longHorn;

    void Shrinking()
    {
        float currentSize = transform.localScale.y;
        //shrinkSize + (1 - shrinkSize) * (1 - chargeLate);
        float targetSize = 1 - chargeLate * (1 - shrinkSize);
        float diffSize = targetSize - currentSize;

        transform.localScale += new Vector3(0, diffSize, 0);
        //transform.position += new Vector3(0, diffSize, 0);
    }

    public Color BodyColor
    {
        set
        {
            foreach(var r in renderers)
            {
                r.color = value;
            }
        }
    }

    void FullChargeColor()
    {
        if (chargeLate >= 1 && Time.time % ColorChangePeriod < ColorChangePeriod / 2)
        {
            BodyColor = Color.yellow;
        }
        else
        {
            BodyColor = Color.white;
        }
    }
}
