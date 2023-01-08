using UnityEngine;

class Rotate : MonoBehaviour
{
    [SerializeField]
    int speed, rotX, rotY, rotZ;
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(rotX, rotY, rotZ) * Time.fixedDeltaTime * speed);
    }
}
