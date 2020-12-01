using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ty
{
    public class BurgerCounter : MonoBehaviour
    {
        [SerializeField] bool autoCountMax = true;
        [SerializeField] private int burgerCount = 1;
        [SerializeField] private GameObject messageObject;
        [SerializeField] private List<Transform> humanPositions = new List<Transform>();
        [SerializeField] private GameObject burgerPrefab;
        private List<int> usedHumanPositions = new List<int>();
        private int currentCount = 0;

        public void AddToCount()
        {
            currentCount++;
            if (currentCount >= burgerCount)
            {
                CompleteCount();
            }
        }

        private void Start()
        {
            if (autoCountMax)
            {
                burgerCount = 0;
                foreach (RegularHumanScript human in FindObjectsOfType<RegularHumanScript>())
                {
                    if (human.Satisfiable)
                    {
                        burgerCount++;
                    }
                }
            }
        }

        public Transform GetHumanPosition()
        {
            Transform trn = humanPositions[Random.Range(0, humanPositions.Count)];
            List<int> inList = new List<int>();
            for (int i = 0; i < humanPositions.Count; i++)
            {
                if (!usedHumanPositions.Contains(i))
                {
                    inList.Add(i);
                }
            }
            if (inList.Count > 0)
            {
                int integ = inList[Random.Range(0, inList.Count)];
                trn = humanPositions[integ];
                usedHumanPositions.Add(integ);
            }
            return trn;
        }

        public void ReturnLocation(Transform pos)
        {
            for (int i = 0; i < humanPositions.Count; i++)
            {
                if (humanPositions[i] == pos)
                {
                    usedHumanPositions.Remove(i);
                    break;
                }
            }
        }

        private void CompleteCount()
        {
            messageObject.SetActive(true);
            //Time.timeScale = 0f;
        }

        // Call this to spawn a Burger, call ActivateBurg to turn on physics.
        public GameObject SpawnBurg(Vector3 pos, Quaternion rot)
        {
            return Instantiate(burgerPrefab, pos, rot);
        }

        public void ActivateBurg(GameObject burg, Vector3 vel, Vector3 angleVel)
        {
            if (burg.GetComponent<Rigidbody>())
            {
                burg.GetComponent<Rigidbody>().isKinematic = false;
                burg.GetComponent<Rigidbody>().velocity = vel;
                burg.GetComponent<Rigidbody>().angularVelocity = angleVel;
            }
        }
    }
}