using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour {

    //[SerializeField]
    //GameObject UIinventoryField;
    [SerializeField]
    UIInventory UIinventory;
    [SerializeField]
    GameObject controlElements;


    private void Awake() {
        Inventory playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
        UIinventory.setPlayerInventory(playerInventory);
        playerInventory.onOpen += onInventoryOpen;
        playerInventory.onClose += onInventoryClose;


    }
    void Start() {


        //UIinventoryField.SetActive(false);
    }



    private void onInventoryOpen(List<ItemInInventory> list) {
        controlElements.SetActive(false); // Error?
        UIinventory.open(list);
        //UIinventory.redrawItems();
        //UIinventoryField.SetActive(false);
    }
    private void onInventoryClose(List<ItemInInventory> list) {
        controlElements.SetActive(true); // Error?
        UIinventory.close(list);
        //UIinventoryField.SetActive(true);
    }


}
