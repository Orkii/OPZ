using System.Xml.Linq;
using UnityEngine;

public class ItemInInventory : ISaveLoadable {
    [field: SerializeField]
    public ItemInfo itemInfo { get; protected set; }
    [SerializeField]
    public int count;
    public bool selected = false;
    
    
    
    public ItemInInventory() {
        if (count <= 0) count = 1;
    }
    public ItemInInventory(ItemInfo itemInfo_) {
        itemInfo = itemInfo_;
        if (count <= 0) count = 1;
    }

    public object load(XElement xml) {
        throw new System.NotImplementedException();
    }

    public XElement save() {
        throw new System.NotImplementedException();
    }
}
