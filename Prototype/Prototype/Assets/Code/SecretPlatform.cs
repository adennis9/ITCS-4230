using UnityEngine;
using System.Collections;
using System;

public class SecretPlatform : MonoBehaviour, ITakeDamage
{
    public GameObject DestroyedEffect;
    public int HitsToDestroyBlock = 3;
    public AudioClip DestroySound;

    public void TakeDamage(int damage, GameObject instigator)
    {
        if (DestroyedEffect != null)
        {
            var projectile = instigator.GetComponent<Projectile>();

			if (projectile.name == "Missile(Clone)")
				HitsToDestroyBlock--;
            if (HitsToDestroyBlock == 0)
            {
                Instantiate(DestroyedEffect, transform.position, transform.rotation);
                gameObject.SetActive(false);
                AudioSource.PlayClipAtPoint(DestroySound, transform.position);
            }
        }
    }

}
