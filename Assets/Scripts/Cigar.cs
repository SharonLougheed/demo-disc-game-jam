using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cigar : MonoBehaviour
{
    public float speed;
    public PlayerStats stats;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.TakeDamage(stats.CigarDamage);
        }

        if(!other.gameObject.tag.Equals("Tables"))
        {
            this.gameObject.SetActive(false);
            Destroy(this);
        }
    }
}
