using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointFollower : MonoBehaviour
{
        Animator anim;

        [SerializeField] private GameObject[] waypoints;
        [SerializeField] private float speed = 2f;

        private int currentWaypointIndex = 0;

        private bool _canMove;

        public bool CanMove
        {
                get
                {
                        return anim.GetBool("canMove");
                }
        }

        private void Awake()
        {
                anim = GetComponent<Animator>();
        }

        private void Update()
        {
                if (CanMove)
                {
                        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .01f)
                        {
                                currentWaypointIndex++;
                                if (currentWaypointIndex >= waypoints.Length)
                                {
                                        currentWaypointIndex = 0;
                                }
                        }
                        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
                }                
        }
}
