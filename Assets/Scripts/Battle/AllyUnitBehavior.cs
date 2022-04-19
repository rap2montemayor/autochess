using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AllyUnitBehavior : MonoBehaviour//, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    // [SerializeField] private Canvas canvas;
    // private RectTransform rectTransform;
    // private CanvasGroup canvasGroup;
    public GameObject[] enemies;
    public GameObject target;
    Vector3 origPos;

    int moveSpeed = 200;
    float atkRange = 0.05f;
    float travelled = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        origPos = new Vector3(999,999,999);
    }

    void Awake(){
        // rectTransform = GetComponent<RectTransform>();
        // canvasGroup = GetComponent<CanvasGroup>();
    }
    // Update is called once per frame
    void Update()
    {
        bool empty = true;
        foreach(GameObject y in enemies){
            if(y != null){
                empty = false;
            }
        }
        if(empty){
            Debug.Log("congrats");
        } else {
            target = closest();
            if(origPos == new Vector3(999,999,999)){
                origPos = transform.position;
            } else {
                float travelDistance = landing(origPos, target.transform.position, atkRange);
                Vector3 direction = target.transform.position - origPos;
                if(travelled < travelDistance ){
                    transform.position += direction * Time.deltaTime;
                    travelled = Vector3.Distance(origPos, transform.position);
                } else {
                    Destroy(target);
                    origPos = transform.position;
                    travelled = 0;
                }
            }
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

    // public void OnBeginDrag(PointerEventData eventData){
    //     canvasGroup.alpha = 0.5f;
    //     canvasGroup.blocksRaycasts = false;
    // }
    // public void OnEndDrag(PointerEventData eventData){
    //     canvasGroup.alpha = 1.0f;
    //     canvasGroup.blocksRaycasts = true;
    // }
    // public void OnDrag(PointerEventData eventData){
    //     rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
    // }
}
