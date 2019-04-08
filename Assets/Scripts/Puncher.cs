using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher : MonoBehaviour
{

    public PlayerStats stats;
    public Side side;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool isPunching;
    private bool isRecovering;
    private float startTime;
    private float punchLength;

    public void Punch()
    {
        if (!isPunching)
        {
            isPunching = true;
            startTime = Time.time;
            startPosition = transform.localPosition;
            endPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z + stats.PunchReach);
            punchLength = Vector3.Distance(startPosition, endPosition);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPunching && other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.TakeDamage(stats.PunchDamage);
        }
    }

    private void Update()
    {
        if (isPunching && !isRecovering)
        {
            float travel = (Time.time - startTime) * stats.PunchSpeed;
            float remainingTravel = travel / punchLength;
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, remainingTravel);
            if (transform.localPosition == endPosition)
            {
                isRecovering = true;
                startTime = Time.time;
            }
        }
        else if (isRecovering)
        {
            float travel = (Time.time - startTime) * stats.PunchSpeed;
            float remainingTravel = travel / punchLength;
            transform.localPosition = Vector3.Lerp(endPosition, startPosition, remainingTravel);
            if (transform.localPosition == startPosition)
            {
                isRecovering = false;
                isPunching = false;
            }
        }
    }
}
