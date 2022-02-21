using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Combat;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
            if (InteractWithCombat()) return;
            
            if (InteractWithMovement()) return;
        }

        bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                Fighter fighter = GetComponent<Fighter>();
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (!fighter.CanAttack(target)) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    fighter.Attack(target);
                }
                return true;
            }
            return false;
        }

        bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}