using System.Xml.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ItemInWorld : MonoBehaviour, ISaveLoadable{
    [field: SerializeField]
    public ItemInfo itemInfo { get; protected set; }

    public ItemInWorld() {

    }
    public ItemInWorld(ItemInfo itemInfo_) {
        itemInfo = itemInfo_;
    }

    public object load(XElement xml) {
        if (xml != null) {
            itemInfo.load(xml.Element("itemInfo"));
        }
        gameObject.name = itemInfo.itemName;
        Sprite spriteAsset = Resources.Load<Sprite>(itemInfo.itemIconPath);
        GetComponent<SpriteRenderer>().sprite = spriteAsset;
        return this;
    }

    public XElement save() {
        if (itemInfo = null) return null;
        XElement ret = new XElement("itemInWorld");
        ret.Add(itemInfo.save());
       
        return ret;
    }

    void Start() {
       
        ((ISaveLoadable)this).initISaveLoadable();
        load(null);

    }

    void OnDestroy() {
        ((ISaveLoadable)this).destroyISaveLoadable();
    }

    void Update() {
        
    }
}