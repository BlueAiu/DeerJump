using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    [Header("èkÇﬁêgëÃÇ∆êFÇÃïœâª")]
    [Tooltip("Ç«Ç±Ç‹Ç≈èkÇﬁÇ©")]
    [SerializeField] float shrinkSize = 0.5f;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float ColorChangePeriod = 0.2f;

    void Shrinking()
    {
        float currentSize = transform.localScale.y;
        //shrinkSize + (1 - shrinkSize) * (1 - chargeLate);
        float targetSize = 1 - chargeLate * (1 - shrinkSize);
        float diffSize = targetSize - currentSize;

        transform.localScale += new Vector3(0, diffSize, 0);
        //transform.position += new Vector3(0, diffSize, 0);
    }

    void FullChargeColor()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        }

        if (chargeLate >= 1 && Time.time % ColorChangePeriod < ColorChangePeriod / 2)
        {
            spriteRenderer.color = Color.yellow;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
}
