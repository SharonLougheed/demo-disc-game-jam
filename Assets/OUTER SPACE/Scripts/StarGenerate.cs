using UnityEngine;

public class StarGenerate : MonoBehaviour
{
    //-- Attach this script to the Point Of Origin (POO) for a Star you wish to duplicate one-or-more time(s).
    //-- SPECIAL NOTE: Children (Star GameObjects) must have the scripts "SpriteSnap" and "SpriteTwinkle".

    public GameObject star;                                     // Star which will be duplicated
    public int quantity = 1;                                    // Number of duplicates

	private void Start()
    {
        for(int i = 0; i < quantity; i++)
        {
            GameObject clone = Instantiate(star, transform);
            
            clone.gameObject.SetActive(true);

            float ranX = Random.Range(0.001f, 50.0f) * (Random.Range(0, 2) == 1 ? -1.0f : 1.0f);
            float ranY = Random.Range(0.001f, 50.0f) * (Random.Range(0, 2) == 1 ? -1.0f : 1.0f);
            float ranZ = Random.Range(0.001f, 50.0f) * (Random.Range(0, 2) == 1 ? -1.0f : 1.0f);

            clone.transform.localPosition = new Vector3(ranX, ranY, ranZ);

            clone.GetComponent<SpriteSnap>().PsyBlowie();
            clone.GetComponent<SpriteTwinkle>().JumpStart();
        }
    }
}