using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private Vector2 startingPosition;
    private float t0;

    public void OnReuseObject(Vector2 startingPosition)
    {
        t0 = Time.frameCount;
        this.startingPosition = startingPosition;
        transform.position = startingPosition;
    }

    void Update()
    {
        float t = Time.frameCount - t0;
        transform.position = Move(t);
    }

    private Vector2 Move(float t)
    {
        return new Vector2(-t / 2, 0) + startingPosition;
    }
}
