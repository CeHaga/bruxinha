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

    public void PickUpHeal()
    {
        OnHeal?.Invoke(1, () => PickUpScore(scoreOnFullHeal));
    }

    public void PickUpBomb()
    {
        OnPickBomb?.Invoke(1, () => PickUpScore(scoreOnFullBomb));
    }

    public void PickUpShootUpgrade()
    {
        OnShootUpgrade?.Invoke(() => PickUpScore(scoreOnFullShootUpgrade));
    }

    public void PickUpScore(int score)
    {
        OnScoreGained?.Invoke(score);
    }
}
