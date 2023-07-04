using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarController : MonoBehaviour
{


    [SerializeField] Vector2 minMaxSizeRange;


    [ContextMenu(nameof(SetRandomSize))]
    public void SetRandomSize()
    {
        var newScale = transform.localScale;

        newScale.x = Random.Range(minMaxSizeRange.x, minMaxSizeRange.y);

            transform.localScale = newScale;
    }
}
