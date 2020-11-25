using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Ty
{
    public class RegularHumanScript : MonoBehaviour
    {
        [SerializeField] private bool satisfiable = true;
        [SerializeField] private bool guard = false;
        [SerializeField] Transform sightPos;
        [SerializeField] float sightTime = 0.6f;
        private GameObject playerRef;
        private bool satisfied = false;
        private Animator anim;
        private NavMeshAgent agent;
        private BurgerCounter brgRef;
        private Transform targetPos;
        private float moveTimer = -1f;
        private float guardTimer = -1f;

        private void Start()
        {
            anim = GetComponent<Animator>();
            brgRef = FindObjectOfType<BurgerCounter>();
            agent = GetComponent<NavMeshAgent>();
            playerRef = FindObjectOfType<Camera>().gameObject;
            SelectLocation();
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, targetPos.position) <= 1)
            {
                if (moveTimer == -1)
                {
                    moveTimer = Random.Range(1.8f, 3.5f);
                }
                SetAnimBool("Moving", false, false);
                print("Location Arrived At.");
            }
            if (moveTimer <= 0 && moveTimer > -1)
            {
                print("Selecting New Location.");
                SelectLocation();
                moveTimer = -1f;
            }
            else if (moveTimer > 0)
            {
                moveTimer -= Time.deltaTime;
            }
        }

        private void CheckSight()
        {
            if (CanSeePlayer())
            {
                if (guardTimer == -1)
                {
                    guardTimer = sightTime;
                }
                else if (guardTimer <= 0)
                {
                    ConfirmSeePlayer();
                }
                else
                {
                    guardTimer -= Time.deltaTime;
                }
            }
            else
            {
                guardTimer = -1f;
            }
        }

        private void ConfirmSeePlayer()
        {
            print("Player has been seen.");
            Time.timeScale = 0;
        }

        private bool CanSeePlayer()
        {
            if (Physics.Raycast(sightPos.position, (playerRef.transform.position - sightPos.position), out RaycastHit hit))
            {
                if (hit.collider.gameObject == playerRef)
                {
                    // Check Burger Mode here.

                    return true;
                }
            }
            return false;
        }

        private void SetAnimBool(string valueName, bool trigger = false, bool value = true)
        {
            if (trigger)
            {
                anim.SetTrigger(valueName);
            }
            else
            {
                anim.SetBool(valueName, value);
            }
        }

        private void SelectLocation()
        {
            Transform trn = null;
            if (targetPos)
            {
                trn = targetPos;
            }
            targetPos = brgRef.GetHumanPosition();
            if (trn)
            {
                brgRef.ReturnLocation(trn);
            }
            agent.destination = targetPos.position;
            SetAnimBool("Moving");
        }

        private void BecomeSatisfied()
        {
            satisfied = true;
            FindObjectOfType<BurgerCounter>().AddToCount();
            SetAnimBool("Eat", true);
        }

        public void DisplaySatisifed()
        {

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