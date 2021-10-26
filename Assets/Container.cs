using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public List<Item> inventoryItems;
    public ItemSlotGridDimensioner attachedItemGrid;

    public ItemInstance item1x1;
    public ItemInstance item1x3;
    public ItemInstance item1x4;
    public ItemInstance item2x2;

    // Start is called before the first frame update
    void Start()
    {
        inventoryItems = new List<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Item item, int count)
    {
        Item temp = Instantiate(item);
        inventoryItems.Add(item);
        float tempfloat = item.gridSize.x * item.gridSize.y;
        ItemSlot startSlot = attachedItemGrid.GetItemSlot().GetComponent<ItemSlot>();

        while(!CheckIfFits(temp, startSlot))
        {
            Debug.Log(startSlot.id);
            int tempInt = startSlot.id + 1;
            if (!attachedItemGrid.GetItemSlot(tempInt))
            {
                Debug.Log("not enough room");
                return;
            }

            startSlot = attachedItemGrid.GetItemSlot(tempInt);
            
        }

        startSlot.itemInSlot = temp;
        startSlot.ItemCount = count;
        startSlot.RefreshInfo();

        if(tempfloat > 1)
        {
            int x = 1;
            int y = 1;

            ItemSlot currentSlotX = startSlot;
            ItemSlot currentSlotY = startSlot;
            while (y < (int)item.gridSize.y)
            {
                //Debug.Log(y);
                currentSlotY.GetNeighbour(GridDirections.DOWN).itemInSlot = temp;
                currentSlotY.GetNeighbour(GridDirections.DOWN).ItemCount = count;
                currentSlotY.RefreshInfo();
                currentSlotY = currentSlotY.GetNeighbour(GridDirections.DOWN);
                y++;
            }
            while  (x < (int)item.gridSize.x)
            {
                y = 1;
                //Debug.Log(x);
                currentSlotX.GetNeighbour(GridDirections.RIGHT).itemInSlot = temp;
                currentSlotX.GetNeighbour(GridDirections.RIGHT).ItemCount = count;
                currentSlotX.RefreshInfo();
                currentSlotX = currentSlotX.GetNeighbour(GridDirections.RIGHT);
                currentSlotY = currentSlotX;
                while (y < (int)item.gridSize.y)
                {
                    //Debug.Log(y);
                    currentSlotY.GetNeighbour(GridDirections.DOWN).itemInSlot = temp;
                    currentSlotY.GetNeighbour(GridDirections.DOWN).ItemCount = count;
                    currentSlotY.RefreshInfo();
                    currentSlotY = currentSlotY.GetNeighbour(GridDirections.DOWN);
                    y++;
                }
                x++;
                
            }
            //currentSlot = startSlot;
            
        }
    }

    public bool CheckIfFits(Item item, ItemSlot startSlot)
    {
        float tempfloat = item.gridSize.x * item.gridSize.y;
        if (tempfloat > 1)
        {
            int x = 1;
            int y = 1;

            ItemSlot currentSlotX = startSlot;
            ItemSlot currentSlotY = startSlot;

            if (!startSlot || startSlot.itemInSlot)
                return false;

            while (y < (int)item.gridSize.y)
            {
                if (currentSlotY.GetNeighbour(GridDirections.DOWN) && currentSlotY.GetNeighbour(GridDirections.DOWN).itemInSlot == null)
                {
                    currentSlotY = currentSlotY.GetNeighbour(GridDirections.DOWN);
                }
                else
                    return false;
                y++;
            }

            while (x < (int)item.gridSize.x)
            {
                y = 1;

                if (currentSlotX.GetNeighbour(GridDirections.RIGHT) && currentSlotX.GetNeighbour(GridDirections.RIGHT).itemInSlot == null)
                {
                    currentSlotX = currentSlotX.GetNeighbour(GridDirections.RIGHT);
                }
                else
                    return false;

                currentSlotY = currentSlotX;
                while (y < (int)item.gridSize.y)
                {
                    if (currentSlotY.GetNeighbour(GridDirections.DOWN) && currentSlotY.GetNeighbour(GridDirections.DOWN).itemInSlot == null)
                    {
                        currentSlotY = currentSlotY.GetNeighbour(GridDirections.DOWN);
                    }
                    else
                        return false;
                    y++;
                }
                x++;
            }
            return true;
        }
        else
        {
            if (startSlot && startSlot.itemInSlot == null)
                return true;
            else
                return false;
        }
        
    }
}
