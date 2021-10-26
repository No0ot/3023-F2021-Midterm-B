using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instantiates prefabs to fill a grid
[RequireComponent(typeof(GridLayout))]
public class ItemSlotGridDimensioner : MonoBehaviour
{
    [SerializeField]
    ItemSlot itemSlotPrefab;

    [SerializeField]
    Vector2Int GridDimensions = new Vector2Int(6, 6);

    public ItemSlot[] itemSlotList;

    void Awake()
    {
        CreateGrid();
    }

    public ItemSlot GetItemSlot()
    {
        foreach(ItemSlot itemSlot in itemSlotList)
        {
            if(!itemSlot.itemInSlot)
            {
                return itemSlot;
            }
        }
        return null;
    }

    void CreateGrid()
    {
        itemSlotList = new ItemSlot[GridDimensions.x * GridDimensions.y + 1];

        for (int y = 0, i = 0; y < GridDimensions.y; y++)
        {
            for (int x = 0; x < GridDimensions.x; x++)
            {
                CreateItemSlot(x, y, i++);
            }
        }
    }

    void CreateItemSlot(int x, int y, int i)
    {
        ItemSlot newObject =  itemSlotList[i] = Instantiate(itemSlotPrefab, this.transform);
        newObject.gridCoordinate = new Vector2(x, y);
        newObject.id = i;

        if (x > 0)
        {
            newObject.SetNeighbour(GridDirections.LEFT, itemSlotList[i - 1]);
        }
        if (y > 0)
        {
            newObject.SetNeighbour(GridDirections.UP, itemSlotList[i - GridDimensions.x]);
        }
    }

    public ItemSlot GetItemSlot(int id)
    {
        if (itemSlotList[id] != null)
            return itemSlotList[id];
        else
            return null;
    }
}
