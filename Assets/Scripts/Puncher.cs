using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher : MonoBehaviour
{
    public PlayerStats stats;
    public Side side;

    public AudioClip playerHitClip;
    public AudioClip objectHitClip;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool isPunching;
    private bool isRecovering;
    private float startTime;
    private float punchLength;

    public AudioClip sfxPunch1;
    public AudioClip sfxPunch2;
    public AudioClip sfxPunch3;

    public AudioClip sfxSqueak;

    public AudioClip sfxStab1;
    public AudioClip sfxStab2;

    public AudioClip sfxFlick;

    public WeaponType weaponType = WeaponType.BareFisted;
    public GameObject cigar;
    public GameObject cigarPrefab;
    public int StrikeCount = 0;
    public int FlingCount = 0;

    public UserInterface userInterface;
    public GameObject[] players; //Set for cigar by player, probably a better way, but too tired

    private void Update()
    {
        if (isPunching && !isRecovering)
        {
            float travel = (Time.time - startTime) * stats.PunchSpeed;
            float remainingTravel = travel / punchLength;
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, remainingTravel);
            if (transform.localPosition == endPosition)
            {
                isRecovering = true;
                startTime = Time.time;
            }
        }
        else if (isRecovering)
        {
            userInterface.ChangePaws(side, false, weaponType);
            float travel = (Time.time - startTime) * stats.PunchSpeed;
            float remainingTravel = travel / punchLength;
            transform.localPosition = Vector3.Lerp(endPosition, startPosition, remainingTravel);
            if (transform.localPosition == startPosition)
            {
                isRecovering = false;
                isPunching = false;
            }
        }
    }

    public void Punch()
    {
        if (!isPunching)
        {
            isPunching = true;
            userInterface.ChangePaws(side, true, weaponType);
            startTime = Time.time;
            startPosition = transform.localPosition;
            switch (weaponType)
            {
                case WeaponType.BareFisted:
                    endPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z + stats.PunchReach);
                    break;
                case WeaponType.Bottle:
                    endPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z + stats.BottleReach);
                    break;
                case WeaponType.Bone:
                    endPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z + stats.BoneReach);
                    break;
                case WeaponType.Cigar:
                    //endPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z + stats.CigarReach);
                    cigar = Instantiate(cigarPrefab, transform.position, transform.rotation);
                    cigar.GetComponentInChildren<SpriteRotator>().allPlayers = players;
                    cigar.GetComponent<Cigar>().ThrowingPlayer = gameObject.GetComponentInParent<Player>();
                    UseFling();
                    break;
                default:
                    break;
            }
            punchLength = Vector3.Distance(startPosition, endPosition);
        }
    }

    public void PickupWeapon(WeaponType newWeapon)
    {
        weaponType = newWeapon;
        userInterface.StartFlashScreen(Color.yellow);

        //ChangeSprite Here
        switch (weaponType)
        {
            case WeaponType.BareFisted:
                StrikeCount = 0;
                break;
            case WeaponType.Bottle:
                StrikeCount = stats.BottleStrikes;
                break;
            case WeaponType.Bone:
                StrikeCount = stats.BoneStrikes;
                break;
            case WeaponType.Cigar:
                FlingCount = stats.CigarFlings;
                break;
            default:
                break;
        }

        userInterface.ChangeWeaponImage(weaponType);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPunching && !isRecovering)
        {
            if (other.gameObject.tag.Equals("Player")
                && !gameObject.transform.parent.gameObject.Equals(other.gameObject))
            {
                Player player = other.gameObject.GetComponent<Player>();
                if (player.IsAlive)
                {
                    DamagePlayer(player);
                    var hitSound = GetComponent<AudioSource>();

                    hitSound.clip = PickSound();

                    hitSound.Play();
                }
            }
            else if (other.gameObject.tag.Equals("BOOMHOOCH"))
            {
                var hitSound = GetComponent<AudioSource>();

                hitSound.clip = objectHitClip;
                hitSound.Play();

                BoomHoochActivate killSwitch = other.gameObject.GetComponent<BoomHoochActivate>();
                killSwitch.GoBoomBoom();
            }
            else if (other.gameObject.tag.Equals("JUKE BOX"))
            {
                var hitSound = GetComponent<AudioSource>();

                hitSound.clip = objectHitClip;
                hitSound.Play();

                other.gameObject.GetComponent<JukeBox>().SmashJuke();
            }
            else
            {
                var hitSound = GetComponent<AudioSource>();
                hitSound.clip = objectHitClip;
                hitSound.Play();
            }


            if (!gameObject.transform.parent.gameObject.Equals(other.gameObject))
            {
                UseStrike();
            }
        }
    }

    private AudioClip PickSound()
    {
        var clipChoice = playerHitClip;
        switch (weaponType)
        {
            case WeaponType.BareFisted:
                switch (UnityEngine.Random.Range(0, 3))
                {
                    case 0:
                        clipChoice = sfxPunch1;
                        break;

                    case 1:
                        clipChoice = sfxPunch2;
                        break;

                    case 2:
                        clipChoice = sfxPunch3;
                        break;
                }
                break;

            case WeaponType.Bone:
                clipChoice = sfxSqueak;
                break;

            case WeaponType.Bottle:
                switch (UnityEngine.Random.Range(0, 3))
                {
                    case 0:
                        clipChoice = sfxStab1;
                        break;

                    case 1:
                        clipChoice = sfxStab1;
                        break;

                    case 2:
                        clipChoice = sfxStab2;
                        break;
                }
                break;
        }

        return clipChoice;
    }

    private void DamagePlayer(Player player)
    {
        switch (weaponType)
        {
            case WeaponType.BareFisted:
                player.TakeDamage(stats.PunchDamage);
                break;
            case WeaponType.Bottle:
                player.TakeDamage(stats.BottleDamage);
                break;
            case WeaponType.Bone:
                player.TakeDamage(stats.BoneDamage);
                break;
            case WeaponType.Cigar:
                player.TakeDamage(stats.CigarDamage);
                break;
            default:
                break;
        }
    }

    private void UseStrike()
    {
        if (weaponType != WeaponType.BareFisted)
        {
            StrikeCount--;
            if (StrikeCount <= 0)
            {
                StrikeCount = 0;
                PickupWeapon(WeaponType.BareFisted);
            }
        }
    }

    private void UseFling()
    {
        if (weaponType == WeaponType.Cigar)
        {
            FlingCount--;
            if (FlingCount <= 0)
            {
                FlingCount = 0;
                PickupWeapon(WeaponType.BareFisted);
            }

            GetComponent<AudioSource>().PlayOneShot(sfxFlick);
        }
    }
}
