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

    public WeaponType weaponType = WeaponType.BareFisted;
    public Cigar cigarPrefab;
    public int StrikeCount = 0;
    public int FlingCount = 0;

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
                    cigarPrefab = Instantiate(cigarPrefab, transform.position, transform.rotation);
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPunching && !isRecovering)
        {
            UseStrike();
            if (other.gameObject.tag.Equals("Player")
                & !gameObject.transform.parent.gameObject.Equals(other.gameObject))
            {
                Player player = other.gameObject.GetComponent<Player>();
                DamagePlayer(player);
                var hitSound = GetComponent<AudioSource>();
                hitSound.clip = playerHitClip;
                hitSound.Play();
            }
            else
            {
                var hitSound = GetComponent<AudioSource>();
                hitSound.clip = objectHitClip;
                hitSound.Play();
            }
        }
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
        }
    }
}
