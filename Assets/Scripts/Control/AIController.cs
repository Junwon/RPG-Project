using UnityEngine;

using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Fighter fighter;
        Health health;
        GameObject player;

        void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
        }

        void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRange(player) && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        bool InAttackRange(GameObject obj)
        {
            float distanceToObj = Vector3.Distance(obj.transform.position, transform.position);
            return distanceToObj < chaseDistance;
        }


        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
