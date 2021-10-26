using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    GameObject containerCanvas;

    [SerializeField]
    ItemTable itemTable;

    public Container itemContainer;

    private void Start()
    {
        itemTable.AssignItemIDs();
        AddItem(0, 1);
        AddItem(1, 2);
        AddItem(3, 3);
        AddItem(3, 4);
        AddItem(3, 5);
        AddItem(0, 1);
        AddItem(0, 1);
        AddItem(0, 1);
    }

    public void OpenContainer()
    {
        containerCanvas.SetActive(true);
    }

    public void CloseContainer()
    {
        containerCanvas.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       // if (collision.gameObject.tag == "Container")
        {
            OpenContainer();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
      //  if (collision.gameObject.tag == "Container")
        {
            CloseContainer();
        }
    }

    void AddItem(int id, int count)
    {
        Item temp = Instantiate(itemTable.GetItem(id));
        itemContainer.AddItem(temp, count);
    }
}
