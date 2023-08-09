using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private string[] collisionTags;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Array.IndexOf(collisionTags, other.gameObject.tag) != -1)
        {
            Debug.Log($"{gameObject.name} hit by {other.gameObject.name}");
        }
    }
}
