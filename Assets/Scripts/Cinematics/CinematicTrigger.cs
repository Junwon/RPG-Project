using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool triggered = false;

        void OnTriggerEnter(Collider other)
        {
            if (!triggered && other.gameObject.tag == "Player")
            {
                triggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}

