using System;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public float pickUpDistance = 3f;
    [Range(1, 5)]
    public int force = 1;
    [SerializeField] private Transform _holdTF, _propsTF;
    private Rigidbody _holdingItemRB;
    private Collider _holdingItemCol;
    private bool _isHolding = false;
    public event Action IsHoldingChanged;
    public bool IsHolding
    {
        get { return _isHolding; }
        set
        {
            if (value != _isHolding)
            {
                _isHolding = value;
                IsHoldingChanged?.Invoke();
            }
            
        }
    }
    
    
    private void Update()
    {
        if (!_isHolding && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            TryTakeItem();
    }
    public void DropItem()
    {
        _holdingItemRB.isKinematic = false;
        _holdingItemCol.isTrigger = false;
        _holdingItemCol.transform.SetParent(_propsTF);
        _holdingItemRB.AddForce(transform.forward * 100f * force);

        _holdingItemCol = null;
        _holdingItemRB = null;

        IsHolding = false;
    }
    private void TryTakeItem()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out RaycastHit hit, pickUpDistance))
        {
            if (hit.collider.CompareTag("Item"))
            {
                Debug.Log("Подобран объект: " + hit.collider.name);
                hit.collider.transform.SetParent(_holdTF);
                hit.collider.transform.localPosition = Vector3.zero;
                _holdingItemRB = hit.collider.GetComponent<Rigidbody>();
                _holdingItemRB.isKinematic = true;
                _holdingItemCol = hit.collider;
                _holdingItemCol.isTrigger = true;
                IsHolding = true;
            }
        }
    }
}
