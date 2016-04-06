using UnityEngine;
using System.Collections;
using System;

public class SimpleEnemyAI : MonoBehaviour, ITakeDamage, IPlayerRespawnListener
{

    public float Speed;
    public float FireRate;
    public int PointsToGivePlayer;
    public Projectile Projectile;
    public GameObject DestroyedEffect;
    public AudioClip EnemyShootSound;
    public AudioClip DestroySound;
    private CharacterController2D _controller;
    private Vector2 _direction;
    private Vector2 _startPosition;
    private float _canFireIn;

    public void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        _direction = new Vector2(-1, 0);
        _startPosition = transform.position;
    }

    public void Update()
    {
        _controller.SetHorizontalForce(_direction.x * Speed);

        if ((_direction.x < 0 && _controller.State.IsCollidingLeft) || _direction.x > 0 && _controller.State.IsCollidingRight)
        {
            _direction = -_direction;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if ((_canFireIn -= Time.deltaTime) > 0)
            return;
        var rayCast = Physics2D.Raycast(transform.position, _direction, 10, 1 << LayerMask.NameToLayer("Player"));

        if (!rayCast)
            return;

        var projectile = (Projectile)Instantiate(Projectile, transform.position, transform.rotation);
        projectile.Initialize(gameObject, _direction, _controller.Velocity);
        _canFireIn = FireRate;
        if (EnemyShootSound != null)
            AudioSource.PlayClipAtPoint(EnemyShootSound, transform.position);
    }

    public void TakeDamage(int damage, GameObject instigator)
    {
        var projectile = instigator.GetComponent<Projectile>();
        var owner = projectile.Owner.GetComponent<Player>();
        if (PointsToGivePlayer != 0)
        {
            
            if (projectile != null && owner != null)
            {
                GameManager.Instance.AddPoints(PointsToGivePlayer);
				if (PointsToGivePlayer != 0)
					FloatingText.Show(string.Format("+{0}!", PointsToGivePlayer), "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50));
            }
        }


        Instantiate(DestroyedEffect, transform.position, transform.rotation);
        if (DestroySound != null)
            AudioSource.PlayClipAtPoint(DestroySound, transform.position);
        gameObject.SetActive(false);


        

    }

    public void OnPlayerRespawnInThisCheckpoint(Checkpoint checkpoint, Player player)
    {
        _direction = new Vector2(-1, 0);
        transform.localScale = new Vector3(1, 1, 1);
        gameObject.SetActive(true);
    }
}
