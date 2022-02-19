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
            InteractWithCombat();
            InteractWithMovement();
        }

        void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        GetComponent<Fighter>().Attack(target);
                    }
                }
            }
        }

        void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        void MoveToCursor()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }

        static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}