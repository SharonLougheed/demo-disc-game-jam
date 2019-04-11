using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public int id = 1;
    public float GroundLevel = 0.5f;

    private void Update()
    {
        if (transform.position.y != GroundLevel)
        {
            transform.position = new Vector3(transform.position.x, GroundLevel, transform.position.z);
        }
    }

    void OnDrawGizmos()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.color = new Color(1, 0, 0, 0.75f);
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.DrawSphere(new Vector3(0, 0, 0.6f), 0.3f);
    }
}
