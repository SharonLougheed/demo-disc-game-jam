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
        if (transform.position.x > 500 || transform.position.y > 500 || transform.position.z > 500)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.TakeDamage(stats.CigarDamage);
            var hitSound = GetComponent<AudioSource>();
            //hitSound.clip = playerHitClip;
            //hitSound.Play();
        }
        else
        {
            var hitSound = GetComponent<AudioSource>();
            //hitSound.clip = objectHitClip;
            //hitSound.Play();
        }
        this.gameObject.SetActive(false);
    }
}
