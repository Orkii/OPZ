using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu(fileName = "CreatureInfoData", menuName = "custom/CreatureInfoData")]
public class CreatureInfo : ScriptableObject, ISaveLoadable {


    [SerializeField]
    public float speed;
    [SerializeField]
    public float HP;
    [SerializeField]
    public string myName;
    [SerializeField]
    public float damage;
    [SerializeField]
    public float attackInterval;
    [SerializeField]
    public List<GameObject> canDrop;


    public object load(XElement xml) {
        Debug.Log("Load xml " + xml);
        speed = float.Parse( xml.Element("speed").Value );
        HP = float.Parse(xml.Element("HP").Value);
        myName = xml.Element("myName").Value;
        damage = float.Parse(xml.Element("damage").Value);
        attackInterval = float.Parse(xml.Element("attackInterval").Value);

        XElement canDrop_ = xml.Element("speed");
        List<XElement> drops = canDrop_.Elements("drop").ToList<XElement>();

        foreach (XElement item in drops) {
            canDrop.Add(AssetDatabase.LoadAssetAtPath<GameObject>(item.Value));
        }

        return this;
    }

    public XElement save() {
        XElement ret = new XElement("creatureInfo");
        //ret.Add("assetPath", AssetDatabase.GetAssetPath(this));

        ret.Add(new XElement("speed", speed));
        ret.Add(new XElement("HP", HP));
        ret.Add(new XElement("myName", myName));
        ret.Add(new XElement("damage", damage));
        ret.Add(new XElement("attackInterval", attackInterval));
        XElement canDrop_ = new XElement("canDrop");
        ret.Add(canDrop_);
        foreach (GameObject item in canDrop) {
            canDrop_.Add("drop", AssetDatabase.GetAssetPath(item));
        }
        return ret;

    }
}
