using System.Collections.Generic;
using UnityEngine;

//Allows Player to select different stuff. USE ONLY IN PLAYER PREFAB
[RequireComponent(typeof(Collider2D))]
public class Selector : MonoBehaviour
{
    private HashSet<Collider2D> nearby;

    //Delay between input
    private float lastPressTime;

    void Start()
    {
        nearby = new HashSet<Collider2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), transform.parent.GetComponent<Collider2D>());
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            Select();
        }
    }

    // If selector encounters an activator, add it to list of nearby activators
    private void OnTriggerEnter2D(Collider2D other) {
        Activator activator = other.GetComponent<Activator>();
        if (activator != null){
            nearby.Add(other);
        }
    }

    //remove out-of-range activators
    private void OnTriggerExit2D(Collider2D other) {
        nearby.Remove(other);
    }

    //when "select" input is received, activate nearest activator

    public void Select(){
        if (nearby.Count > 0){
             GetNearestActivator().Activate();
        }
    }

    //get nearest activator
    private Activator GetNearestActivator(){
        double nearestDistance = 999d;
        Collider2D nearestOther = null;
        foreach (Collider2D other in nearby){
            double newDistance = Vector2.Distance(other.transform.position, transform.position);
            if (newDistance < nearestDistance){
                nearestOther = other;
                nearestDistance = newDistance;
            }
        }
        return nearestOther.GetComponent<Activator>();
    }

    private bool InputDelayIsDone(){
        return InputDelayIsDone(0.1f);
    }
    private bool InputDelayIsDone(float delay_time){
        if (Time.fixedTime - lastPressTime >= delay_time){
            lastPressTime = Time.fixedTime;
            return true;
        }else{
            return false;
        }
    }
}