using UnityEngine;

public class SpaceChase : MonoBehaviour
{
    //-- Attach this script to an OUTER SPACE that needs to follow a Camera.

    public GameObject chasee;                                   // The Camera that the OUTER SPACE should chase
    public float chaseRadius;                                   // Distance at which chasing will commence

    private void OnValidate()
    {
        if(chaseRadius < 0)
        {
            chaseRadius *= -1;
        }
    }

    private void LateUpdate()
    {
        float distance = Vector3.Distance(transform.position, chasee.transform.position);

        if(distance > 0)
        {
            transform.position += (chasee.transform.position - transform.position) * ((distance - chaseRadius) / distance);
        }
	}
}