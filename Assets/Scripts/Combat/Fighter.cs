using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour 
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;

        void Update()
        {
            if(target == null) return;

            if(!IsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Stop();
            }
        }

        public void Attack(CombatTarget combatTarget)
        {   
            target = combatTarget.transform; 
        }

        public void Cancel()
        {
            target = null;
        }

        bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }
    }
}