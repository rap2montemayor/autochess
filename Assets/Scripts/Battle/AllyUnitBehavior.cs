using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AllyUnitBehavior : MonoBehaviour
{
    public Unit unitDataReference;
    [HideInInspector]
    public Unit unitData;

    public GameObject[] enemies;
    public GameObject target;
    Vector3 origPos;

    float moveSpeed;
    float atkRange;
    float travelled = 0;

    float cooldown = 1;
    float currentCooldown = 0;

    void InitializeUnitData() {
        unitData.amr_current = unitData.amr_max;
        unitData.atkphys_current = unitData.atkphys_max;
        unitData.crt_current = unitData.crt_max;
        unitData.hp_current = unitData.hp_max;
    }

    void Start() {
        origPos = new Vector3(999,999,999);
        unitData = unitDataReference.Copy();
        InitializeUnitData();
        GetComponent<Image>().sprite = unitData.icon;
        atkRange = 0.1f * Mathf.Max(unitData.range, 1);
        moveSpeed = 0.25f * Mathf.Max(unitData.evasion, 1);
    }

    void Update() {
        currentCooldown -= Time.deltaTime;
        currentCooldown = Mathf.Max(currentCooldown, 0);

        bool empty = true;
        foreach(GameObject y in enemies){
            if(y != null) {
                empty = false;
            }
        }
        if(empty){
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        } else {
            target = closest();
            if(origPos == new Vector3(999,999,999)){
                origPos = transform.position;
            } else {
                float travelDistance = landing(origPos, target.transform.position, atkRange);
                Vector3 direction = target.transform.position - origPos;
                if(travelled < travelDistance ){
                    transform.position += direction * Time.deltaTime * moveSpeed;
                    travelled = Vector3.Distance(origPos, transform.position);
                } else {
                    AttackTarget();
                    origPos = transform.position;
                    travelled = 0;
                }
            }
        }
    }

    int CalculateDamage(Unit targetData) {
        int damage = unitData.atkphys_current - targetData.amr_current;
        damage = Mathf.Max(damage, 1);
        return damage;
    }

    bool getCrit() {
        return Random.Range(1, 100) <= unitData.crt_current;
    }

    public bool getEvade() {
        return Random.Range(1, 100) <= unitData.evasion;
    }

    void AttackTarget() {
        if (target == null || currentCooldown > 0) {
            return;
        }
        currentCooldown = cooldown;

        EnemyUnitBehavior targetBehavior = target.GetComponent<EnemyUnitBehavior>();
        Unit targetData = targetBehavior.unitData;

        int myDamage = CalculateDamage(targetData);
        if (getCrit()) {
            myDamage += myDamage/2;
        }
        if (targetBehavior.getEvade()) {
            myDamage = 0;
        }

        targetData.ModHP(-myDamage);
        if (targetData.hp_current == 0) {
            Destroy(target);
            target = null;
        }
    }

    float landing (Vector3 pos, Vector3 enemyPos, float atkRange){
        float distance = Vector3.Distance(pos, enemyPos);
        distance -= atkRange;
        return Mathf.Abs(distance);
    }

    GameObject closest(){
        GameObject closest = null;
        float currentDistance = Mathf.Infinity;
        foreach(GameObject x in enemies){
            if(x!=null){
                float distance = Vector3.Distance(transform.position, x.transform.position);
                if(distance < currentDistance){
                    currentDistance = distance;
                    closest = x;
                }
            }
        }
        return closest;
    }
}
