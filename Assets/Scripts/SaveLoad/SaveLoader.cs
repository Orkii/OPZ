using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SaveLoader {


    private static string path => Application.persistentDataPath + "/saveData.txt";


    static List<ISaveLoadable> needToSaveLoad = new List<ISaveLoadable>();



    public static void save() {
        XElement root = new XElement("root");
        foreach (ISaveLoadable ob in needToSaveLoad) {
            root.Add(ob.save());
        }
        root.Save(path);
    }
    public static void load() {

    }

    public static void addToSave(ISaveLoadable el) {
        if (el == null) return;
        if (needToSaveLoad.Contains(el)) return;
        needToSaveLoad.Add(el);
    }
    public static void removeFromSave(ISaveLoadable el) {
        needToSaveLoad.Remove(el);
    }
}
