using NUnit.Framework.Constraints;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[System.Serializable]
public class ItemInfo : ISaveLoadable {

    [SerializeField]
    public string itemName;
    [SerializeField]
    public string itemIconPath;
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



public class ItemInInventory : MonoBehaviour, ISaveLoadable {

    protected ItemInfo itemInfo;

    public object load(XElement xml) {
        throw new System.NotImplementedException();
    }

    public XElement save() {
        throw new System.NotImplementedException();
    }
}