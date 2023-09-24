using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPickup : MonoBehaviour
{
    public HealEvent OnHeal;
    [SerializeField] private int scoreOnFullHeal;
    public PickBombEvent OnPickBomb;
    [SerializeField] private int scoreOnFullBomb;
    public ShootUpgradeEvent OnShootUpgrade;
    [SerializeField] private int scoreOnFullShootUpgrade;
    public UpdateScoreCountEvent OnScoreGained;

    public void PickUpHeal(){
		AudioManager.Instance.Play("SFX_PickUpHeal");
        OnHeal?.Invoke(1, () => PickUpScore(scoreOnFullHeal));
    }

    public void PickUpBomb(){
		AudioManager.Instance.Play("SFX_PickUpUpgrade");
        OnPickBomb?.Invoke(1, () => PickUpScore(scoreOnFullBomb));
    }

    public void PickUpShootUpgrade(){
		AudioManager.Instance.Play("SFX_PickUpUpgrade");
        OnShootUpgrade?.Invoke(() => PickUpScore(scoreOnFullShootUpgrade));
    }

    public void PickUpScore(int score){
		AudioManager.Instance.Play("SFX_PickUpScore");
        OnScoreGained?.Invoke(score);
    }
}
