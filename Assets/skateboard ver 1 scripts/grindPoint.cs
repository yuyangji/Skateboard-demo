using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skate
{
    public class grindPoint : MonoBehaviour
    {
        private float speed = 13.0f;
        TriggerArea triggerArea;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject opening_point;
        [SerializeField] private GameObject closing_point;

        // Start is called before the first frame update
        void Start()
        {
            triggerArea = GetComponentInChildren<TriggerArea>();
        }

        // Update is called once per frame
        void Update()
        {
            if (playerInTriggerArea())
            {
                player.GetComponent<playerMovementController>().grinding = true;
                Vector3 dir = (player.transform.position - closing_point.transform.position).normalized;
                float dot = Vector3.Dot(dir, transform.forward);
                if (dot < 0)
                {
                    Vector3 _toCurrentWaypoint = opening_point.transform.position - Vector3.MoveTowards(transform.position, opening_point.transform.position, Time.deltaTime * speed);

                    //Get normalized movement direction;
                    Vector3 _movement = _toCurrentWaypoint.normalized;

                    //Get movement for this frame;
                    _movement *= speed * Time.deltaTime;

                    player.GetComponent<playerMovementController>().grindVector = triggerArea.rigidbodiesInTriggerArea[0].position + _movement;
                }
                else
                {
                    Vector3 _toCurrentWaypoint = closing_point.transform.position - Vector3.MoveTowards(transform.position, closing_point.transform.position, Time.deltaTime * speed);

                    //Get normalized movement direction;
                    Vector3 _movement = _toCurrentWaypoint.normalized;

                    //Get movement for this frame;
                    _movement *= speed * Time.deltaTime;

                    player.GetComponent<playerMovementController>().grindVector = triggerArea.rigidbodiesInTriggerArea[0].position + _movement;
                }                
            }
        }

        private bool playerInTriggerArea()
        {
            if (triggerArea.rigidbodiesInTriggerArea.Contains(player.GetComponent<Rigidbody>())) {
                return true;
            }
            else return false;
        }
    }
}