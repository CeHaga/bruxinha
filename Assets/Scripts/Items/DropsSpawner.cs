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
    public int probability;
    public UnityEvent OnPickup;
}

public class DropsSpawner : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] private DropsOptions[] itemsOptions;
    private int[] itemsProbabilitySum;
    private ObjectPool<ItemController>[] itemPool;

    private bool isGamePaused;

    private void Start()
    {
        itemPool = new ObjectPool<ItemController>[itemsOptions.Length];
        itemsProbabilitySum = new int[itemsOptions.Length];
        int sum = 0;
        for (int i = 0; i < itemsOptions.Length; i++)
        {
            itemPool[i] = createObjectPool(itemsOptions[i], i, 1, 5);
            sum += itemsOptions[i].probability;
            itemsProbabilitySum[i] = sum;
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

            itemController.Init((item) => OnOutOfBounds(item, itemIndex), () => isGamePaused);

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
        int random = Random.Range(0, 100);
        int itemIndex = -1;
        for (int i = 0; i < itemsProbabilitySum.Length; i++)
        {
            if (random < itemsProbabilitySum[i])
            {
                itemIndex = i;
                break;
            }
        }
        if (itemIndex == -1) return;
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

    public void PauseDrops(bool isGamePaused)
    {
        this.isGamePaused = isGamePaused;
    }
}
