using UnityEngine;

public class SpriteSnap : MonoBehaviour
{
    //-- Attach this script to a SPRITE you wish to snap to a particular distance from a Point Of Origin (POO).

    public float globeRadius = 50.0f;                           // Distance at which the SPRITE will be snapped

    public void PsyBlowie()
    {
        Vector3 epicCenter = Vector3.zero;

        if(Vector3.Distance(epicCenter, transform.localPosition) != 0.0f)
        {
            transform.localPosition *= (globeRadius / Vector3.Distance(epicCenter, transform.localPosition));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Wiggle(float scaler)
    {
        float xScale = scaler * (Random.Range(0, 2) == 1 ? -1.0f : 1.0f);
        float yScale = scaler * (Random.Range(0, 2) == 1 ? -1.0f : 1.0f);

        transform.localScale = new Vector3(xScale, yScale, 0);
    }
}