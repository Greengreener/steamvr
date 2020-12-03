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
        [SerializeField] GameObject hatRef;

        public bool Satisfiable { get { return satisfiable; } }
        public bool Guard { get { return guard; } }

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
            if (FindObjectOfType<HeadScript>())
            {
                playerRef = FindObjectOfType<HeadScript>().gameObject;
            }
            else
            {
                playerRef = FindObjectOfType<Camera>().gameObject;
            }
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
                //print("Location Arrived At.");
            }
            if (moveTimer <= 0 && moveTimer > -1)
            {
                //print("Selecting New Location.");
                SelectLocation();
                moveTimer = -1f;
            }
            else if (moveTimer > 0)
            {
                moveTimer -= Time.deltaTime;
            }

            if (guard)
            {
                CheckSight();
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
            if (Vector3.Angle(sightPos.position, playerRef.transform.position) > 120)
            {
                print("Outside sight angle.");
                return false;
            }
            if (!Physics.Raycast(sightPos.position, (playerRef.transform.position - sightPos.position), Vector3.Distance(sightPos.position, playerRef.transform.position)))
            {
                if (playerRef.GetComponent<HeadScript>())
                {
                    if (playerRef.GetComponent<HeadScript>().BurgerTime)
                    {
                        return false;
                    }
                }
                return true;
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
            PauseMovement();
        }

        private void PauseMovement()
        {
            agent.destination = transform.position;
            SetAnimBool("Moving", false, false);
        }

        private void ResumeMovement()
        {
            if (targetPos)
            {
                agent.destination = targetPos.position;
                SetAnimBool("Moving");
            }
        }

        public void DisplaySatisifed()
        {
            hatRef.SetActive(true);
            ResumeMovement();
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