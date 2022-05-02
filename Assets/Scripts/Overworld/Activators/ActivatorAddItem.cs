// When you activate this, obtain an item and display a short message
// In practice the actual use of this is severely limited. I don't know yet what else to recommend though.

public class ActivatorAddItem : Activator
{
    public Item item;
    private bool doOnce = false;
    
    public override void Activate(){
        if (!doOnce){
            string message = "You got " + item.name_data + "!";
            Dialogue.SayLine(message);
            Inventory.instance.items.Add(item);
            doOnce = true;
        }
    }
}