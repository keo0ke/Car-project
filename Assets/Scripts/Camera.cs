using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private Vector3 offset = new Vector3(0, 2, -3); // Смещение: вверх и назад
    [SerializeField] private float smoothSpeed = 5f; // Плавность следования

    void LateUpdate() // Используем LateUpdate для более плавного следования
    {
        if (car == null) return;

        // Получаем позицию машины
        Vector3 carPosition = car.transform.position;

        // Получаем направление, куда смотрит машина
        Vector3 carForward = car.transform.forward;
        Vector3 carRight = car.transform.right;
        Vector3 carUp = car.transform.up;

        // Вычисляем позицию камеры с учетом поворота машины
        // offset.x - смещение вправо/влево
        // offset.y - смещение вверх/вниз
        // offset.z - смещение вперед/назад
        Vector3 targetPosition = carPosition
                                + carRight * offset.x
                                + carUp * offset.y
                                + carForward * offset.z;

        // Плавно перемещаем камеру
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Камера смотрит на машину
        transform.LookAt(car.transform.position + carForward * 2f); // Смотрим немного вперед машины
    }
}