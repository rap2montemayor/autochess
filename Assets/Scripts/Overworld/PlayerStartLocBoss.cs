using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* To be attached to Player object, will set player's location to appropriate place once scene is loaded */

public class PlayerStartLocBoss : MonoBehaviour
{
    private PlayerStartLoc[] playerStartLocs; 
 

    void Start(){
        int startLocID = SceneLoader.GetStartLocID();
        playerStartLocs = FindObjectsOfType<PlayerStartLoc>();

        if (startLocID != 0){
            foreach (PlayerStartLoc startLoc in playerStartLocs){
                if (startLoc.id == startLocID){
                    gameObject.transform.position = startLoc.GetLocation();
                }
            }
        }
    }
}