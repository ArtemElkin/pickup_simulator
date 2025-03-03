using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public float sensivity;
    public bool yInversion;
    [SerializeField] private Transform _playerBody;
    private float _xRotation;
    public int _cameraTouchId = -1;
    public int _joystickTouchId = -1;


    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            // Если тач отпущен, освобождаем его ID
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (touch.fingerId == _cameraTouchId)
                    _cameraTouchId = -1;

                if (touch.fingerId == _joystickTouchId)
                    _joystickTouchId = -1;
            }
            // Проверяем, не относится ли тач к уже назначенным пальцам
            if (touch.fingerId == _joystickTouchId) 
                continue; // Этот тач управляет джойстиком, игнорируем его в коде камеры

            // Проверяем, не был ли этот тач на UI при старте
            if (touch.phase == TouchPhase.Began)
            {
                if (IsTouchingUI(touch))
                {
                    _joystickTouchId = touch.fingerId; // Назначаем этот палец для джойстика
                    continue; // Не даём ему стать тачем для камеры
                }
                else if (_cameraTouchId == -1)
                {
                    _cameraTouchId = touch.fingerId; // Если камера свободна, назначаем
                }
            }

            // Если палец управляет камерой и двигается
            if (touch.fingerId == _cameraTouchId && touch.phase == TouchPhase.Moved)
            {
                float _deltaX = touch.deltaPosition.x * sensivity;
                float _deltaY = touch.deltaPosition.y * sensivity * (yInversion ? -1 : 1);

                // Поворачиваем персонажа
                _playerBody.Rotate(Vector3.up * _deltaX);

                // Ограничиваем угол наклона камеры по оси X
                _xRotation = Mathf.Clamp(_xRotation - _deltaY, -60f, 60f);
                transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            }

            
        }
    }
    private bool IsTouchingUI(Touch touch)
    {
        return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
    }
}
