using UnityEngine;

namespace RPG.Core
{
    public class CameraFacing : MonoBehaviour
    {
        void LateUpdate()
        {
            transform.LookAt(transform.position + Camera.main.transform.forward);
        }
    }
}
