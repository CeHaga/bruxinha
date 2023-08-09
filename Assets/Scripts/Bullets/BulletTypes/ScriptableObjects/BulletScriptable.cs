using UnityEngine;

[CreateAssetMenu(fileName = "BulletScriptable", menuName = "bruxinha/BulletScriptable", order = 0)]
public class BulletScriptable : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private BulletController bulletPrefab;

    public BulletController BulletPrefab => bulletPrefab;

    public override string ToString()
    {
        return name;
    }
}
