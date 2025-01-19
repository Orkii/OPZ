using System.Xml.Linq;
using UnityEngine;

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



    public object load(XElement xml) {
        throw new System.NotImplementedException();
    }

    public XElement save() {
        throw new System.NotImplementedException();
    }
}
