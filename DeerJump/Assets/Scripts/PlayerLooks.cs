using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    [Header("èkÇﬁêgëÃ")]
    [Tooltip("Ç«Ç±Ç‹Ç≈èkÇﬁÇ©")]
    [SerializeField] float shrinkSize = 0.5f;

    void Shrinking()
    {
        float currentSize = transform.localScale.y;
        //shrinkSize + (1 - shrinkSize) * (1 - chargeLate);
        float targetSize = 1 - chargeLate * (1 - shrinkSize);
        float diffSize = targetSize - currentSize;

        transform.localScale += new Vector3(0, diffSize, 0);
        transform.position += new Vector3(0, diffSize, 0);
    }
}
