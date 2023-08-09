using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class ShootEvent : UnityEvent<BulletScriptable, Vector2> { };
