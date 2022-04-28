using UnityEngine;
public class ActivatorGacha : Activator
{
    public GachaUI gachaUI;

    public override void Activate(){
        gachaUI.Show();
    }

}