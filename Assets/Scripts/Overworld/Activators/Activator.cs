using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Activator : MonoBehaviour
{
    public abstract void Activate();
}