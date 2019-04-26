using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public int id = 1;
    public float GroundLevel = 0.5f;
    public SpawnType spawnType = SpawnType.Player;

    private void Awake()
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
        switch (spawnType)
        {
            case SpawnType.Player:
                Gizmos.color = new Color(1, 0, 0, 0.75f);
                break;
            case SpawnType.Health:
                Gizmos.color = new Color(0, 1, 0, 0.75f);
                break;
            case SpawnType.Weapon:
                Gizmos.color = new Color(0, 0, 1, 0.75f);
                break;
            default:
                Gizmos.color = new Color(1, 0, 0, 0.75f);
                break;
        }
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.DrawSphere(new Vector3(0, 0, 0.6f), 0.3f);
    }
}
