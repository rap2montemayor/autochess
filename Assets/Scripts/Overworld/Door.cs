using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
    private Collider2D doorCollider;
    public string nextScene;
    public int nextDoorID;
    private bool doOnce = false;

    void OnTriggerEnter2D(Collider2D other){
        if (!doOnce){
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null){
                PlayerController.deactivate();
                SceneLoader.LoadScene(nextScene, nextDoorID);
            }
        }
    }
}