using UnityEngine;

public class CameraScript : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}
