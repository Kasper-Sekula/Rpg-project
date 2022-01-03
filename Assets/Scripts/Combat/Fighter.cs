using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        Transform target;
        float timeSinceLastAttack = 0;

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

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
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // This will trigger the Hit() event.
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        // Animation Event
        void Hit()
        {
            if (target == null) return;
            target.GetComponent<Health>().TakeDamage(weaponDamage);
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
    }
}