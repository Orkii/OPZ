using System.Xml.Linq;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
        if (itemInfo == null) itemInfo = (ItemInfo)ScriptableObject.CreateInstance("ItemInfo");
        if (xml == null) return this;
        Debug.Log("itemInInventory load\n " + xml);
        if (xml != null) {
            itemInfo.load(xml.Element("itemInfo"));
        }
        Debug.Log("itemInInventory =" + xml);
        count = int.Parse(xml.Element("count").Value);
        return this;
    }

    public XElement save() {
        Debug.Log("ItemInWorld Save");
        if (itemInfo == null) {
            Debug.Log("ItemInWorld itemInfo = null");
            return null;
        }
        XElement ret = new XElement("itemInInventory");
        ret.Add(itemInfo.save());
        ret.Add(new XElement("count", count));
        return ret;
    }
}
