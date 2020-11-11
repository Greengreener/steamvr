using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ty
{
    public class BurgerCounter : MonoBehaviour
    {
        [SerializeField] int burgerCount = 1;
        [SerializeField] GameObject messageObject;
        int currentCount = 0;

        public void AddToCount()
        {
            currentCount++;
            if (currentCount >= burgerCount)
            {
                CompleteCount();
            }
        }

        private void CompleteCount()
        {
            messageObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}