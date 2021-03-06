using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GachaUI : MonoBehaviour{

    [SerializeField]
    private Text rollsLeftText;

    [SerializeField]
    private Button rollButton;

    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private Image prizeImage;

    [SerializeField]
    private Text prizeText;

    [SerializeField]
    private GachaDatabase gachaDatabase;

    private CanvasGroup canvasGroup;

    private Sprite default_icon;

    private bool showing;

    [SerializeField]
    private int rollsLeft;

    public void Start(){
        default_icon = prizeImage.sprite;
        canvasGroup = GetComponent<CanvasGroup>();
        showing = false;
        UpdateRollsLeft();
        Hide();
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            if (showing) {
                OnButtonExit();
            } else {
                Show();
            }
        }
    }

    public void Show(){
        showing = true;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }

    public void Hide(){
        showing = false;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }

    public void OnButtonRoll(){
        if (rollsLeft > 0){
            // roll
            Unit new_unit = gachaDatabase.PullUnit();

            // display new unit
            prizeImage.sprite = new_unit.icon;
            prizeText.text = "Congrats, you got " + new_unit.name_data + "!";

            // add it to inventory
            Inventory.instance.units.Add(new_unit);
            rollsLeft--;
            UpdateRollsLeft();
        }
    }
    
    public void OnButtonExit(){
        prizeImage.sprite = default_icon;
        prizeText.text = "";
        Hide();
    }

    public void UpdateRollsLeft(){
        rollsLeftText.text = "Pulls left: " + rollsLeft;
    }
}