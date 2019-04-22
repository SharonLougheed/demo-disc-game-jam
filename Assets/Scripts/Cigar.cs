using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cigar : MonoBehaviour
{
    public float speed;
    public PlayerStats stats;
    private void Start()
    {
    }

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
        Destroy(this);
    }
}
