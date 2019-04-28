using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cigar : MonoBehaviour
{
    public float speed;
    public PlayerStats stats;

    public GameObject concusser;
    public GameObject cigarImage;

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
            StartCoroutine(Immolate());
        }
    }

    IEnumerator Immolate()
    {
        concusser.SetActive(true);
        cigarImage.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);

        yield return null;
    }
}
