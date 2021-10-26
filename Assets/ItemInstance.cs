using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInstance : MonoBehaviour
{
    Item reference;

    public ItemInstance(Item itemref)
    {
        reference = itemref;
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = reference.icon;
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
