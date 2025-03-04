using UnityEngine;

public class GateButton : MonoBehaviour
{
    public Gate gateToOpen;


    public void Activate()
    {
        gateToOpen.Switch();
    }
}
