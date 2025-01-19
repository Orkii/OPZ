using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ItemInWorld : MonoBehaviour, ISaveLoadable{
    [field: SerializeField]
    public ItemInfo itemInfo { get; protected set; }
    [SerializeField]
    public SpriteRenderer spriteRenderer;
    public ItemInWorld() {

    }
    public ItemInWorld(ItemInfo itemInfo_) {
        itemInfo = itemInfo_;
    }

    public object load(XElement xml) {
        if (itemInfo == null) itemInfo = (ItemInfo)ScriptableObject.CreateInstance("ItemInfo");
        if (xml == null) return this;
        Debug.Log("ItemInWorld load\n " + xml);
        if (xml != null) {
            itemInfo.load(xml.Element("itemInfo"));
        }
        Debug.Log("ItemInWorld =" + xml);
        transform.position = Utility.StringToVector3(xml.Element("position").Value);
        spriteRenderer.sprite = itemInfo.image;
        return this;
    }

    public XElement save() {
        Debug.Log("ItemInWorld Save");
        if (itemInfo == null) {
            Debug.Log("ItemInWorld itemInfo = null");
            return null;
        }
        XElement ret = new XElement("itemInWorld");
        ret.Add(itemInfo.save());
        ret.Add(new XElement("position", transform.position));
        Debug.Log("ItemInWorld ret = \n" + ret);
        return ret;
    }

    void Start() {
        Debug.Log("Item start");
        ((ISaveLoadable)this).initISaveLoadable();
        load(null);
    }

    void OnDestroy() {
        ((ISaveLoadable)this).destroyISaveLoadable();
    }

    void Update() {
        
    }
}