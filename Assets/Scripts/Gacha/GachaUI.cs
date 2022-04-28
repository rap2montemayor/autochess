using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GachaUI : MonoBehaviour{

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

    public void Start(){
        default_icon = prizeImage.sprite;
        canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    public void Show(){
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }
    public void Hide(){
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }

    public void OnButtonRoll(){
        // roll
        Unit new_unit = gachaDatabase.PullUnit();

        // display new unit
        prizeImage.sprite = new_unit.icon;
        prizeText.text = "Congrats, you got " + new_unit.name_data + "!";

        // add it to inventory
        Inventory.instance.units.Add(new_unit);
    }
    
    public void OnButtonExit(){
        prizeImage.sprite = default_icon;
        prizeText.text = "";
        Hide();
    }
}