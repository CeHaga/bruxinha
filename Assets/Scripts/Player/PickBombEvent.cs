using UnityEngine;
using UnityEngine.Events;
using System;
[System.Serializable]
public class PickBombEvent : UnityEvent<int, Action> { };
