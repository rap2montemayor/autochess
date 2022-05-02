using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rgbd;
    //private Animator animator;

    private int base_speed;
    private int multiplier;
    private Vector2 movement;


    //so that multiple scripts can deactivate the playercontroller, and player can only start moving once they've all reactivated
    private static int numDisable;

    public static void activate(){
        if (numDisable > 0){numDisable--;}
    }
    public static void deactivate(){
        numDisable++;
    }
  
    void Start(){
        rgbd = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        base_speed = 6;
        multiplier = 1;
        movement = Vector2.zero;
        numDisable = 0;
    }

    void FixedUpdate(){
        if (numDisable == 0){
            movement = Vector2.zero;
            multiplier = 1;

            //animator.SetFloat("multiplier", 1f);
            
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");             
            
            // EVENTUALLY please fix this
            //animator.SetInteger("x", (int)Mathf.Ceil(movement.x));
            //animator.SetInteger("y", (int)Mathf.Ceil(movement.y));
            
            if (Input.GetKey("left shift")){
                multiplier *= 2;
                //animator.SetFloat("multiplier", 2f);
            }

            rgbd.MovePosition(rgbd.position + (movement * Time.fixedDeltaTime * base_speed * multiplier));
        }
    }
}