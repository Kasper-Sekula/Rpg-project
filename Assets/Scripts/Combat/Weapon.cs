using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject 
    {
        [SerializeField] AnimatorOverrideController weaponOverride = null;
        [SerializeField] GameObject weaponPrefab = null;

        public void Spawn(Transform handTranform, Animator animator)
        {
            Instantiate(weaponPrefab, handTranform);
            animator.runtimeAnimatorController = weaponOverride;
        }
        
    }
}