using UnityEngine;

public class FollowMousePosition : MonoBehaviour
{
    private Canvas canvas;
    void Awake(){
        canvas = FindObjectOfType<Canvas>();
        Debug.Log("Follow... activated!");
    }
    void Update()
    {
         Vector2 pos;
         RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
         transform.position = canvas.transform.TransformPoint(pos);
    }
}
