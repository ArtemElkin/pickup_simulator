using UnityEngine;

public class GateOpener : MonoBehaviour
{
    public float interactDistance = 5f;
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            TryOpenGate();
    }
    private void TryOpenGate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.CompareTag("Gate Button"))
            {
                GateButton gateButton = hit.collider.GetComponent<GateButton>();
                gateButton.Activate();
                Debug.Log("Button Pressed! Opening...");
            }
        }
    }
}
