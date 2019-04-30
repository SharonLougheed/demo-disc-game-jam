using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cigar : MonoBehaviour
{
    public float speed;
    public PlayerStats stats;
    public Player ThrowingPlayer;

    public GameObject concusser;
    public GameObject cigarImage;
    public GameObject sparkler;

    private bool isImmolating = false;

    void Update()
    {
        if (!isImmolating)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("Tables") && !isImmolating)
        {
            if (other.gameObject.tag.Equals("Player")
                    && !ThrowingPlayer.Equals(other.GetComponent<Player>()))
            {
                other.gameObject.GetComponent<Player>().TakeDamage(stats.CigarDamage);
            }

            if (other.gameObject.tag.Equals("JUKE BOX"))
            {
                other.GetComponent<JukeBox>().SmashJuke();
            }

            StartCoroutine(Immolate());
        }
    }

    IEnumerator Immolate()
    {
        isImmolating = true;

        concusser.SetActive(true);
        sparkler.SetActive(true);
        cigarImage.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);

        yield return null;
    }
}
