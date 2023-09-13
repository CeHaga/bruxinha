using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;

    [Header("Collision")]
    [SerializeField] private string[] collisionTags;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Array.IndexOf(collisionTags, other.gameObject.tag) == -1) return;

        healthManager?.CollidedWithTag(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerEnter2D(other);
    }
}
