using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIInventory : MonoBehaviour {
    Inventory playerInventory;

    [SerializeField]
    List<UnityEngine.UI.Image> cells;// = new List<SpriteRenderer>();
    void Start() {
        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
        playerInventory.onChange += onInventoryChanged;

        Debug.Log("cells = " + cells);
    }

    private void onInventoryChanged(List<ItemInInventory> list) {
        Debug.Log("Inventory changed");
        Debug.Log("cells count = " + cells.Count);
        Debug.Log("cells = " + cells);
        Debug.Log("cells0 = " + cells[0]);
        //Debug.Log("cells1 = " + cells[1]);
        //Debug.Log("cells2 = " + cells[2]);
        //Debug.Log("cells3 = " + cells[3]);
        //Debug.Log("cells4 = " + cells[4]);
        Sprite a = cells[0].sprite = list[0].itemInfo.image;
    }
}
