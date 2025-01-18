using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIInventory : MonoBehaviour {
    Inventory playerInventory;

    [SerializeField]
    private GameObject cellExample;
    [SerializeField]
    private GameObject UIcells;
    protected List<UIInventoryCell> cells = new List<UIInventoryCell>();

    public delegate void ItemHandler(ItemInInventory item);



    protected void onCellClick(ItemInInventory item) {
        Debug.Log("Someone was cliced");
        playerInventory.setSelectedItem(item);
        redrawItems();
    }

    public void setPlayerInventory(Inventory playerInventory_) {
        playerInventory = playerInventory_;
    }

    private void Awake() {
        
    }
    void Start() {
        for (int i = 0; i < playerInventory.capacity; i++) {
            GameObject newCell = GameObject.Instantiate(cellExample);
            newCell.transform.SetParent(UIcells.transform);
            UIInventoryCell uiCell = newCell.GetComponent<UIInventoryCell>();
            uiCell.onClick += onCellClick;
            cells.Add(uiCell);
        }

        playerInventory.onChange += onInventoryChanged;
        gameObject.SetActive(false);
    }

    private void onInventoryChanged(List<ItemInInventory> list) {
        Debug.Log("Inventory changed");
        Debug.Log("list.Count = " + list.Count);

        drawItems(list);
    }

    private void drawItems(List<ItemInInventory> list) {
        Debug.Log("Inventory changed");
        Debug.Log("list.Count = " + list.Count);
        foreach (UIInventoryCell cell in cells) {
            cell.removeSprite();
        }
        for (int i = 0; i < list.Count; i++) {
            Debug.Log("CELL = " + i);
            cells[i].removeSprite();
            cells[i].setSprite(list[i]);
            //cells[i].draw();
        }
        redrawItems();
    }
    public void redrawItems() {
        foreach (UIInventoryCell cell in cells) {
            cell.draw();
        }
    }

    public void open(List<ItemInInventory> list) {
        gameObject.SetActive(true);
        drawItems(list);
    }
    public void close(List<ItemInInventory> list) {
        gameObject.SetActive(false);
        playerInventory.setSelectedItem(null);
    }
}
