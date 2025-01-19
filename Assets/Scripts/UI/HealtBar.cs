using UnityEngine;

public class HealtBar : MonoBehaviour {

    [SerializeField]
    GameObject maxHPBar;
    [SerializeField]
    GameObject curentHPBar;
    [SerializeField]
    Destractable destractable;
    float maxHP = 0;



    private void Start() {
        Debug.Log("HealtBar Start");
        maxHP = destractable.maxHP;
        destractable.onTakeDamage += changeHP;
        changeHP(destractable.takeHP());
    }

    private void changeHP(object sender, System.EventArgs args) {
        Debug.Log("changeHP");
        float nowHP = float.Parse(((OnDieEventArgs)args).Text);
        changeHP(nowHP);
    }
    private void changeHP(float nowHP) {
        float percentage = nowHP / maxHP;
        curentHPBar.transform.localScale = new Vector2(percentage * maxHPBar.transform.localScale.x, curentHPBar.transform.localScale.y);
    }

    

}
