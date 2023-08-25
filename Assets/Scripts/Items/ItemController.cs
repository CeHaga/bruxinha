using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemController : MonoBehaviour
{
    private Vector2 startingPosition;
    private float t0;
    private Action<ItemController> OnOutOfBounds;

    public void Init(Action<ItemController> OnOutOfBounds)
    {
        this.OnOutOfBounds = OnOutOfBounds;
    }

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
        CheckOutOfBounds();
    }

    private Vector2 Move(float t)
    {
        return new Vector2(-t / 2, 0) + startingPosition;
    }

    private void CheckOutOfBounds()
    {
        if (transform.position.x < -130)
        {
            OnOutOfBounds?.Invoke(this);
        }
    }
}
