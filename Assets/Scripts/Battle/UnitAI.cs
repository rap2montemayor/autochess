using System.Collections;  
using System.Collections.Generic;  
using UnityEngine; 

public class UnitAI : MonoBehaviour {

    private enum State {
        Moving,
        Attacking,
        Paused,
    }

    [SerializeField]
    private float attackRange;
    private State state;
    public Board unit;
    public Unit unitstats;
    List<UnitAI> list;

    public void Start(){
        state = State.Paused;
        //Get all Units on Board into list
        
    }

    public void Update(){
        switch (state) {
            default:
            case State.Paused:
                // idle
                break;
            case State.Moving:
                // unit moves to find enemy
                findTarget();
                break;
            case State.Attacking:
                // opposing unit is in range, start attacking
                enemyinRange();
                break;
        }
    }

    public void enemyinRange(){
       /*if(getEnemyUnitPosition => attackRange){
           //Apply a dmg formula against target
       }
       */
    }

    public void findTarget() {
        /*if(getEnemyUnitPosition <= attackRange){
            //Pathfinding AI
        }
        */
    }

}