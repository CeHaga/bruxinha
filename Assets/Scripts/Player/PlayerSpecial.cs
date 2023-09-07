using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class PlayerSpecial : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    private int bombCount;
    [SerializeField] private int maxBombs;
    [SerializeField] private UpdateSpecialCountEvent onBombUsed;
    [SerializeField] private UpdateSpecial updateSpecial;

    private void Start()
    {
        bombCount = maxBombs;
        onBombUsed.AddListener(updateSpecial.UpdateText);
        updateSpecial.UpdateText(bombCount);
    }

    public void OnLaunchBomb(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!bomb.activeSelf && bombCount > 0)
            {
                bomb.transform.position = this.transform.position;
                bomb.SetActive(true);
                bombCount--;
                onBombUsed.Invoke(bombCount);
            }
            else
            {
                Debug.Log("Sem bombas ou bomba ativa");
                //Som sinalizando que não tem como lançar uma bomba
            }
        }
    }

    public void OnRestoreBomb(int bombCount, Action callback = null)
    {
        if (bombCount == maxBombs)
        {
            callback?.Invoke();
            return;
        }
        this.bombCount = Mathf.Min(this.bombCount + bombCount, maxBombs);
        onBombUsed.Invoke(this.bombCount);
    }
}
