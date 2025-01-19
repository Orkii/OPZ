using NUnit.Framework.Constraints;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

//[System.Serializable]
[CreateAssetMenu(fileName = "itemInfoData", menuName = "custom/itemInfoData")]
public class ItemInfo : ScriptableObject, ISaveLoadable {

    [SerializeField]
    public string itemName;
    [SerializeField]
    public string itemIconPath;
    [SerializeField]
    protected Sprite image_;

    public Sprite image
    {
        get { 
            if (image_ == null) image_ = Resources.Load<Sprite>(itemIconPath);
            return image_;
        }
    }

    public object load(XElement xml) {
        throw new System.NotImplementedException();


    }

    public XElement save() {
        XElement ret = new XElement("itemInfo");
        ret.Add(new XElement("itemName", itemName));
        ret.Add(new XElement("itemIconPath", itemIconPath));
        return ret;
    }
}



