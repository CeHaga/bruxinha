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
        if (level < 1) level = 1;
        if (level > bulletLevelImages.Length) level = bulletLevelImages.Length;
        bulletLevelHUD.sprite = bulletLevelImages[level - 1];
    }
}
