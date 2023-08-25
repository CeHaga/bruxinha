using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPickup : MonoBehaviour
{
    public HealEvent OnHeal;
    public PickBombEvent OnPickBomb;
    public UnityEvent OnShootUpgrade;

    public void PickUpHeal()
    {
        OnHeal?.Invoke(1);
    }

    public void PickUpBomb()
    {
        OnPickBomb?.Invoke(1);
    }

    public void PickUpShootUpgrade()
    {
        OnShootUpgrade?.Invoke();
    }

    public void PickUpScore(int score)
    {
        Debug.Log($"Picking up score {score} - TODO FUNCTION");
    }
}
