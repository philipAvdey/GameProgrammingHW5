using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{

    public Transform player;

    void LateUpdate()
    {
        Vector3 newPos = player.position;
        newPos.y = transform.position.y;
        transform.position = newPos;
    }

}
