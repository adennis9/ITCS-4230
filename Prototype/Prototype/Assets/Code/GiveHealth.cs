using UnityEngine;
using System.Collections;
using System;

public class GiveHealth : MonoBehaviour, IPlayerRespawnListener
{
    public GameObject Effect;
    public int HealthToGive;
    public float timeToLive = 1.5f;
    public int speed = 50;
   public Animator Animator;
   public SpriteRenderer Renderer;

   private bool _isCollected;


    public void OnTriggerEnter2D(Collider2D other)
    {

       if (_isCollected)
          return;

        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        player.GiveHealth(HealthToGive, gameObject);
        Instantiate(Effect, transform.position, transform.rotation);
        FloatingText.Show(string.Format("+{0}!", HealthToGive), "PlayerGotHealthText", new FromWorldPointTextPositioner(Camera.main, transform.position, timeToLive, speed));

        gameObject.SetActive(false);

        _isCollected = true;

        Animator.SetTrigger("Collect");
        gameObject.SetActive(false);

    }

    public void FinishAnimationEvent()
    {
        Renderer.enabled = false;
        Animator.SetTrigger("Reset");
    }

    public void OnPlayerRespawnInThisCheckpoint(Checkpoint checkpoint, Player player)
    {
        _isCollected = false;
        Renderer.enabled = true;
        Debug.Log("Respawn");
        gameObject.SetActive(true);
    }
}
