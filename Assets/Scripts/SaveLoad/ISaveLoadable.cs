using System.Xml.Linq;
using UnityEngine;

public interface ISaveLoadable {
    public abstract object load(XElement xml);
    public abstract XElement save();

    public void initISaveLoadable() {
        SaveLoader.addToSave(this);
    }
    public void destroyISaveLoadable() {
        SaveLoader.removeFromSave(this);
    }
}
