using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerSpecial : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private int bombCount;
    [SerializeField] private UpdateSpecialCountEvent onBombUsed;
    [SerializeField] private UpdateSpecial updateSpecial;

    private void Start()
    {
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
}
