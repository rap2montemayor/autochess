using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartLoc : MonoBehaviour
{
    private Vector2 location;
    public int id;

    protected void Start(){
        location = transform.position;
    }

    public Vector2 GetLocation(){
        return location;
    }
}