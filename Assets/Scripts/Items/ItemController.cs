using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemController : MonoBehaviour
{
    private Vector2 startingPosition;
    private float t;
    private Action<ItemController> OnOutOfBounds;

    private Func<bool> onGamePaused;

    public void Init(Action<ItemController> OnOutOfBounds, Func<bool> onGamePaused)
    {
        this.OnOutOfBounds = OnOutOfBounds;
        this.onGamePaused = onGamePaused;
    }

    public void OnReuseObject(Vector2 startingPosition)
    {
        //t0 = Time.frameCount;
        t = 0;
        this.startingPosition = startingPosition;
        transform.position = startingPosition;
    }

    void Update()
    {
        if(onGamePaused()) return;
        //float t = Time.frameCount - t0;
        transform.position = Move(t);
        t++;
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
