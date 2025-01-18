using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UIInventory;
using static UnityEditor.Progress;

public class UIInventoryCell : MonoBehaviour {
    [SerializeField]
    protected Image itemImage;
    [SerializeField]
    TextMeshProUGUI text;
    protected Image image;

    public ItemHandler onClick;
    public ItemInInventory item;


    private void Awake() {
        image = GetComponent<Image>();
    }
    private void OnEnable() {
        Debug.Log("OnEnable enter");
        draw();
    }
    public void setSprite(ItemInInventory item_) {
        item = item_;
        draw();
        Debug.Log("setSprite");
        Debug.Log("setSprite = " + item_);
    }
    public void removeSprite() {
        item = null;
        Debug.Log("removeSprite");
        //if (enabled == false) return;
        if (itemImage != null) {
            itemImage.sprite = null;
            itemImage.color = Color.clear;
        }
        image.color = Color.black;
    }
    public Sprite getSprite() {
        return itemImage.sprite;
    }
    public void draw() {
        if (item == null) {
            Debug.Log("Draw null");
            itemImage.sprite = null;
            itemImage.color = Color.clear;
            text.text = "";
            return;
        }

        itemImage.sprite = item.itemInfo.image;
        itemImage.color = Color.white;
        text.text = item.count.ToString();
        itemImage.SetNativeSize();          // Need to do smth

        if (item.selected) {
            image.color = Color.clear;
        }
        else {
            image.color = Color.black;
        }
    }
    void OnMouseDown() {
        Debug.Log("I was clicked");
        onClick.Invoke(item);
    }
    public void click() {
        Debug.Log("I was clicked");
        onClick.Invoke(item);
    }

}