using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgEffect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public void Setup(Vector3 position , Color color)
    {
        transform.position = position;
        spriteRenderer.color = color;
        Invoke("Destroy", 1f);
    }


    private void Destroy()
    {
        Destroy(gameObject);
    }
}
