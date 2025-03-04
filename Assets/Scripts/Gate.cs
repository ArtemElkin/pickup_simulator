using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private float _openedAngle; // Угол в открытом состоянии
    [SerializeField] private float _closedAngle; // Угол в закрытом состоянии
    [SerializeField] private float _rotationSpeed = 90f; // Скорость вращения в градусах в секунду
    [SerializeField] private Transform _gateBase; // Основание ворот
    private bool _isOpened;
    private Coroutine _rotationCoroutine;


    private void Start()
    {
        Vector3 currentRotation = _gateBase.localEulerAngles;
        // Сохраняем Y и Z, устанавливаем только X
        Quaternion newRotation = Quaternion.Euler(_isOpened ? _openedAngle : _closedAngle, currentRotation.y, currentRotation.z);
        _gateBase.localRotation = newRotation;

    }
    public void Switch()
    {
        _isOpened = !_isOpened;
        float targetAngle = _isOpened ? _openedAngle : _closedAngle;   // Изменяем целевой угол
        if (_rotationCoroutine != null) // Останавливаем предыдущее движение, если есть
            StopCoroutine(_rotationCoroutine);
        _rotationCoroutine = StartCoroutine(RotateGate(targetAngle));
    }
    private IEnumerator RotateGate(float targetAngle)
    {
        Quaternion startRotation = _gateBase.localRotation;
        Debug.Log("Start locEulerAngleX:" + _gateBase.localEulerAngles.x);
        Quaternion targetRotation = Quaternion.Euler(targetAngle, _gateBase.localEulerAngles.y, _gateBase.localEulerAngles.z);

        float elapsedTime = 0f;
        float duration = Mathf.Abs(targetAngle - _gateBase.localEulerAngles.x) / _rotationSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            _gateBase.localRotation = Quaternion.Slerp(startRotation, targetRotation, t);

            yield return null;
        }
        // Фиксируем конечное положение
        _gateBase.localRotation = targetRotation;
        _rotationCoroutine = null;
    }
}
