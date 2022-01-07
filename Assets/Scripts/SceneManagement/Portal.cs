using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) {
            print("entered");
            if (other.tag == "Player")
            {
                print("playerenetered");
                SceneManager.LoadScene(1);
            }
        }
    }

}