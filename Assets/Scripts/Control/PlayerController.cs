using UnityEngine;

using RPG.Attributes;
using RPG.Combat;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        enum CursorType
        {
            None,
            Move,
            Combat
        }

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;

        void Awake()
        {
            health = GetComponent<Health>();
        }
        
        void Update()
        {
            if (health.IsDead()) return;

            if (InteractWithCombat()) return;
            
            if (InteractWithMovement()) return;

            SetCursor(CursorType.None);
        }

        bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                Fighter fighter = GetComponent<Fighter>();
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                GameObject targetGameObject = target.gameObject;
                if (!fighter.CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton(0))
                {
                    fighter.Attack(target.gameObject);
                }
                SetCursor(CursorType.Combat);
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
                SetCursor(CursorType.Move);
                return true;
            }
            return false;
        }

        void SetCursor(CursorType type)
        {
            if (cursorMappings.Length > 0)
            {
                CursorMapping mapping = GetCursorMapping(type);
                Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
            }
        }

        CursorMapping GetCursorMapping(CursorType type)
        {
            Debug.Assert(cursorMappings.Length > 0, "cursorMapping is empty. Needs to be populated.");
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }

            return cursorMappings[0];
        }

        Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}