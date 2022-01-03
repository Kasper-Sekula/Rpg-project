using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
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
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            GetComponent<Animator>().SetTrigger("attack");
        }

        public void Attack(CombatTarget combatTarget)
        {   
            GetComponent<ActionScheduler>().StartAction(this);
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

        // Animation Event
        void Hit()
        {

        }
    }
}