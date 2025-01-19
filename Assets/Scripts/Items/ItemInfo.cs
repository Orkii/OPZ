using NUnit.Framework.Constraints;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

//[System.Serializable]
[CreateAssetMenu(fileName = "itemInfoData", menuName = "custom/itemInfoData")]
public class ItemInfo : ScriptableObject, ISaveLoadable {

    [SerializeField]
    public string itemName;
    //[SerializeField]
    //public string itemIconPath;
    [SerializeField]
    public Sprite image;

    public string itemIconPath {
        get { return AssetDatabase.GetAssetPath(image); }
        set { image = AssetDatabase.LoadAssetAtPath<Sprite>(value); }
    }


    public object load(XElement xml) {
        Debug.Log("ItemInfo enter\n" + xml);
        XElement root = xml.Element("itemInfo");
        Debug.Log("ItemInfo root\n" + root);
        itemName = xml.Element("itemName").Value; 
        itemIconPath = xml.Element("itemIconPath").Value;
        return this;
    }

    public XElement save() {
        XElement ret = new XElement("itemInfo");
        ret.Add(new XElement("itemName", itemName));
        ret.Add(new XElement("itemIconPath", itemIconPath));
        return ret;
    }
}



