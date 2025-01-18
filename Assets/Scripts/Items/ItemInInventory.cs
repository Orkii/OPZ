using System.Xml.Linq;
using UnityEngine;

public class ItemInInventory : ISaveLoadable {
    [field: SerializeField]
    public ItemInfo itemInfo { get; protected set; }
    public int count;
    public bool selected = false;
    
    
    
    public ItemInInventory() {

    }
    public ItemInInventory(ItemInfo itemInfo_) {
        itemInfo = itemInfo_;
    }

    public object load(XElement xml) {
        throw new System.NotImplementedException();
    }

    public XElement save() {
        throw new System.NotImplementedException();
    }
}
