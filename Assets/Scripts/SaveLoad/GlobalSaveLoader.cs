using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSaveLoader : MonoBehaviour {

    [SerializeField]
    GameObject JenericitemInWorld;
    [SerializeField]
    GameObject zombie;
    [SerializeField]
    GameObject flesh;

    [SerializeField]
    GameObject playerGameObject;


    int enemyCount = 3;

    private void Awake() {
        XElement root = SaveLoader.load();
        if (root == null) {
            newStart();
            return;
        }
        Debug.Log("GlobalSaveLoader\n" + root);
        List<XElement> itemInWorld = root.Elements("itemInWorld").ToList<XElement>();

        foreach (XElement item in itemInWorld) {
            GameObject obj = GameObject.Instantiate(JenericitemInWorld);
            ItemInWorld worldItem = obj.GetComponent<ItemInWorld>();
            worldItem.load(item);
        }

        List<XElement> enemys = root.Elements("Enemy").ToList<XElement>();

        foreach (XElement item in enemys) {


            string name_ = item.Element("name").Value;
            Debug.Log("load enemy " + name_);
            Debug.Log("load enemy " + item);
            GameObject enemy = null;
            if (name_ == "Flesh") {
                Debug.Log("load Flesh");
                enemy = GameObject.Instantiate(flesh);
            }
            else if (name_ == "Zombie") {
                Debug.Log("load Zombie");
                enemy = GameObject.Instantiate(zombie);
            }
            if (enemy == null) continue;
            enemy.GetComponent<Enemy>().load(item);
        }

        XElement xmlPlayer = root.Element("player");
        Debug.Log("XElement xmlPlayer ");
        if (xmlPlayer != null) {
            Debug.Log("player != null");
            playerGameObject.GetComponent<Player>().load(xmlPlayer);
        }
    }


    private void newStart() {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < enemyCount; i++) {
            int s = Random.Range(0, enemyCount-1);
            if (s == 0) {
                list.Add(GameObject.Instantiate(flesh));
            }
            else if (s == 1) {
                list.Add(GameObject.Instantiate(zombie));
            }
        }

        foreach (GameObject item in list) {
            item.transform.position = new Vector2(
                Random.Range(0, 25),
                Random.Range(-25, 0));
            
        }
    }
    public void restart() {
        SaveLoader.deleteSave();
        SceneManager.LoadScene("Empty");
    }
    void OnApplicationQuit() {
        SaveLoader.save();
    }
}
