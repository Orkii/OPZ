using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Enemy : Creature {
    [SerializeField]
    CreatureInfo creatureInfo;
    [SerializeField]
    Eyes eyes;
    [SerializeField]
    List<GameObject> canDrop;
    [SerializeField]
    ExternCollider damageRadius;

    protected float nextAttack = 0;
    GameObject targetToGo;
    bool canDoDamage;
    //Rigidbody2D rb;
    

    public void onSawSmth(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            targetToGo = col.gameObject;
        }       
    }
    public void onUnSawSmth(Collider2D col) {
        targetToGo = null;
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
    }
    override protected void Update() {
        base.Update();  
        if (targetToGo != null) {
            Vector2 dir = targetToGo.transform.position - transform.position;
            //Debug.Log("dir1 = " + dir);
            dir /= dir.magnitude;
            //Debug.Log("dir2 = " + dir);
            rb.linearVelocity = dir * creatureInfo.speed;
        }
        else rb.linearVelocity = Vector2.zero;

        //Debug.Log("1");
        if ((targetToGo != null) && canDoDamage) {
            Debug.Log("2");
            doDamage();
        }
    }

    protected void dropOnDie(object sender, System.EventArgs args) {
        int i = Random.Range(0, canDrop.Count);
        GameObject drop = Instantiate(canDrop[i]);
        drop.transform.position = transform.position;
    }

    protected void doDamage() {
        Debug.Log("3");
        Debug.Log("nextAttack = " + nextAttack);
        Debug.Log("Time.time  = " + Time.time);
        if (nextAttack <= Time.time) {
            Debug.Log("DO ATTACK");
            nextAttack = Time.time + creatureInfo.attackInterval;
            Destractable destr = targetToGo.GetComponent<Destractable>();
            if (destr != null) {
                Debug.Log("DO DAMAGE");
                destr.doDamage(creatureInfo.damage);
            }
            nextAttack = Time.time + creatureInfo.attackInterval;
        }
    }

    protected void onDamageRadiusEnter(Collider2D col) {
        Debug.Log("onDamageRadiusEnter");
        if (col.tag == "Player") {
            Debug.Log("Player");
            canDoDamage = true;
        }
    }
}
