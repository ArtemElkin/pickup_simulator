using UnityEngine;

public class DropButtonView : MonoBehaviour
{
    [SerializeField] private GameObject _btn;
    [SerializeField] private PickupController _pickupController;

    private void OnEnable()
    {
        _pickupController.IsHoldingChanged += IsHoldingChanged;
    }
    private void OnDisable()
    {
        _pickupController.IsHoldingChanged -= IsHoldingChanged;
    }

    private void IsHoldingChanged()
    {
        _btn.SetActive(_pickupController.IsHolding);
    }
}
