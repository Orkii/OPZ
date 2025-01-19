using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Enemy : Creature, ISaveLoadable {
    [SerializeField]
    CreatureInfo creatureInfo;
    [SerializeField]
    Eyes eyes;
    [SerializeField]
    ExternCollider damageRadius;

    protected float nextAttack = 0;
    GameObject targetToGo;
    bool canDoDamage = false;
    //Rigidbody2D rb;
    

    public void onSawSmth(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            targetToGo = col.gameObject;
        }       
    }
    public void onUnSawSmth(Collider2D col) {
        if (col.gameObject == targetToGo) {
            targetToGo = null;
        }
    }


    override protected void Start() {
        base.Start();
        if (creatureInfo != null) { 
            HP = creatureInfo.HP;
            speed = creatureInfo.speed;
            name = creatureInfo.myName;
        }
        if (eyes != null) {
            eyes.onSee += onSawSmth;
            eyes.onUnSee += onUnSawSmth;
        }
        //rb = GetComponent<Rigidbody2D>();
        onBeforeDie += dropOnDie;
        damageRadius.onEnter += onDamageRadiusEnter;
        ((ISaveLoadable)this).initISaveLoadable();
    }
    override protected void Update() {
        base.Update();  
        if (targetToGo != null) {
            //Debug.Log("targetToGo= " + targetToGo);
            Vector2 dir = targetToGo.transform.position - transform.position;
            //Debug.Log("dir1 = " + dir);
            dir /= dir.magnitude;
            //Debug.Log("dir2 = " + dir);
            rb.linearVelocity = dir * creatureInfo.speed;
        }
        else rb.linearVelocity = Vector2.zero;

        //Debug.Log("1");
        if ((targetToGo != null) && canDoDamage) {
            //Debug.Log("2");
            doDamage();
        }
    }

    protected void dropOnDie(object sender, System.EventArgs args) {
        if (creatureInfo.canDrop == null || creatureInfo.canDrop.Count == 0) return;
        int i = Random.Range(0, creatureInfo.canDrop.Count);
        GameObject drop = Instantiate(creatureInfo.canDrop[i]);
        drop.transform.position = transform.position;
    }

    protected void doDamage() {
        if (nextAttack <= Time.time) {
            nextAttack = Time.time + creatureInfo.attackInterval;
            Destractable destr = targetToGo.GetComponent<Destractable>();
            if (destr != null) {
                destr.doDamage(creatureInfo.damage);
            }
            nextAttack = Time.time + creatureInfo.attackInterval;
        }
    }

    protected void onDamageRadiusEnter(Collider2D col) {
        if (col.tag == "Player") {
            canDoDamage = true;
        }
    }

    public object load(XElement xml) {
        Debug.Log("Enemy load 1");
        if (xml != null) {
            creatureInfo.load(xml.Element("creatureInfo"));
        }
        Debug.Log("Enemy load 2");
        transform.position = Utility.StringToVector3(xml.Element("position").Value);
        return this;
    }

    public XElement save() {
        Debug.Log("Enemy save 1");
        if (creatureInfo == null) return null;
        Debug.Log("Enemy save 2");
        XElement ret = new XElement("Enemy");
        ret.Add(new XElement("name", name));
        ret.Add(creatureInfo.save());
        ret.Add(new XElement("position", transform.position));

        return ret;
    }
    void OnDestroy() {
        ((ISaveLoadable)this).destroyISaveLoadable();
    }
}
