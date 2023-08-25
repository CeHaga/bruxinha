using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;

[System.Serializable]
public struct DropsOptions
{
    public bool canSpawn;
    public GameObject itemPrefab;
    public UnityEvent OnPickup;
}

public class DropsSpawner : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] private DropsOptions[] itemsOptions;
    private ObjectPool<ItemController>[] itemPool;
    [SerializeField] private float itemSpawnChance;

    private void Start()
    {
        itemPool = new ObjectPool<ItemController>[itemsOptions.Length];
        for (int i = 0; i < itemsOptions.Length; i++)
        {
            itemPool[i] = createObjectPool(itemsOptions[i], i, 1, 5);
        }
    }

    private ObjectPool<ItemController> createObjectPool(DropsOptions itemOptions, int itemIndex, int size, int maxSize)
    {
        return new ObjectPool<ItemController>(() =>
        {
            GameObject item = Instantiate(itemOptions.itemPrefab);
            item.name = itemOptions.itemPrefab.name;

            ItemPickup itemPickup = item.GetComponent<ItemPickup>();
            ItemController itemController = item.GetComponent<ItemController>();

            itemPickup.Init((item) => OnPickup(item, itemIndex),
                            itemController);

            itemController.Init((item) => OnOutOfBounds(item, itemIndex));

            return itemController;
        }, (item) =>
        {
            item.gameObject.SetActive(true);
        }, (item) =>
        {
            item.gameObject.SetActive(false);
        }, (item) =>
        {
            Destroy(item.gameObject);
        }, false, size, maxSize);
    }

    public void SpawnItem(Vector2 startingPosition)
    {
        if (Random.Range(0f, 1f) > itemSpawnChance) return;

        int itemIndex = Random.Range(0, itemsOptions.Length);
        ItemController item = itemPool[itemIndex].Get();
        item.OnReuseObject(startingPosition);
    }

    public void OnPickup(ItemController itemController, int itemIndex)
    {
        itemsOptions[itemIndex].OnPickup?.Invoke();
        itemPool[itemIndex].Release(itemController);
    }

    public void OnOutOfBounds(ItemController itemController, int itemIndex)
    {
        itemPool[itemIndex].Release(itemController);
    }
}
