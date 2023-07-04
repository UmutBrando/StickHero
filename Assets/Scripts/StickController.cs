using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickController : MonoBehaviour
{
   
    public Transform colliderPosition;

    [SerializeField] private float speed;
    
    public bool grow { get; set; }
    
    
    
    private void Update()
    {
        if (grow)
        {
            var scale = transform.localScale;
            scale.y += speed * Time.deltaTime;
            transform.localScale = scale;
        }
    }
}
