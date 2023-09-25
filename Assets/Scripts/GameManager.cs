using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string musicToPlay;
    void Start()
    {
        Application.targetFrameRate = 60;
        if (musicToPlay != "") AudioManager.Instance.Play(musicToPlay);
    }
}
