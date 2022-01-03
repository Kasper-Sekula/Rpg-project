using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        void Update()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (DistanceToPlayer() < chaseDistance)
            {
                print(gameObject.name + " should chase " + player.name);
            }    
        }

        private float DistanceToPlayer()
        {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(transform.position, player.transform.position);
        }
    }
}