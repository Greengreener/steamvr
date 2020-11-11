using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ty
{
    public class RegularHumanScript : MonoBehaviour
    {
        bool satisfied = false;

        private void BecomeSatisfied()
        {
            satisfied = true;
            FindObjectOfType<BurgerCounter>().AddToCount();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("brger") && !satisfied)
            {
                BecomeSatisfied();
                Destroy(other.gameObject);
            }
        }
    }
}