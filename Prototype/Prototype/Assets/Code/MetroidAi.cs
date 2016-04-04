using UnityEngine;
using System.Collections;
using System;

public class MetroidAi : MonoBehaviour, ITakeDamage, IPlayerRespawnListener
{

    public float Speed;
    public float MaxHealth = 100;
    public AudioClip DestroySound;

    private CharacterController2D _controller;
    private Vector2 _direction;



    public void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        _direction = new Vector2(-1, 0);
    }

    public void Update()
    {
        _controller.SetHorizontalForce(_direction.x * Speed);

        if ((_direction.x < 0 && _controller.State.IsCollidingLeft) || _direction.x > 0 && _controller.State.IsCollidingRight)
        {
            _direction = -_direction;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if (!_controller.CanJump)
            return;

        _controller.Jump();

    }


    public void TakeDamage(int damage, GameObject instigator)
    {
        var projectile = instigator.GetComponent<Projectile>();
        var owner = projectile.Owner.GetComponent<Player>();
        var damageInput = projectile.GetComponent<SimpleProjectile>().Damage;

        if (owner != null)
            MaxHealth -= damageInput;

            if (projectile != null && owner != null && MaxHealth <= 0)
            {
                FloatingText.Show("Metroid Defeated!", "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50));
                gameObject.SetActive(false);

            if (DestroySound != null)
                AudioSource.PlayClipAtPoint(DestroySound, transform.position);
            }
    }


    public void OnPlayerRespawnInThisCheckpoint(Checkpoint checkpoint, Player player)
    {
        _direction = new Vector2(-1, 0);
        transform.localScale = new Vector3(1, 1, 1);
        gameObject.SetActive(true);
    }
}
