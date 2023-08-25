using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletLevel : MonoBehaviour
{
    [SerializeField] private Sprite[] bulletLevelImages;
    [SerializeField] private Image bulletLevelHUD;

    public void UpdateBulletLevel(int level)
    {
        bulletLevelHUD.sprite = bulletLevelImages[level - 1];
    }
}
