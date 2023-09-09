using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ItemPickup : MonoBehaviour
{
    private Action<ItemController> OnPickup;
    private ItemController itemController;

    public void Init(Action<ItemController> OnPickup, ItemController itemController)
    {
        this.OnPickup = OnPickup;
        this.itemController = itemController;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerPickup"))
        {
            OnPickup(itemController);
        }
    }
}
