using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour, ITakeDamage
{
    private bool _isFacingRight;
    private CharacterController2D _controller;
    private float _normalizedHorizontalSpeed;

    public float MaxSpeed = 8;
    public float SpeedAcclerationOnGround = 10f;
    public float SpeedAcclerationInAir = 5f;
    public float DamageTextLifeSpan = 1.5f;
    public float DamageTextSpeed = 50;
    public int MaxHealth = 100;
    public GameObject OuchEffect;
    public Projectile Projectile;
    public float FireRate;
    public Transform ProjectileFireLocation;
    public GameObject FireProjectileEffect;
    public AudioClip PlayerHitSound;
    public AudioClip PlayerShootSound;
    public AudioClip PlayerHealthSound;
    public Animator Animator;

    public int Health { get; private set; }
    public bool IsDead { get; private set; }
    public Projectile CurrentProjectile { get; set; }
    private float _canFireIn;

    public void Awake()
    {
        _controller = GetComponent < CharacterController2D>();
        _isFacingRight = transform.localScale.x > 0;
        Health = MaxHealth;
        CurrentProjectile = Projectile;

    }

    public void Update()
     {
        _canFireIn -= Time.deltaTime;

        if (!IsDead)
            HandleInput();

        var movementFactor = _controller.State.IsGrounded ? SpeedAcclerationOnGround : SpeedAcclerationInAir;
        if (IsDead)
            _controller.SetHorizontalForce(0);
        else
        _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));

        Animator.SetBool("IsGrounded", _controller.State.IsGrounded);
        Animator.SetBool("IsDead", IsDead);
        Animator.SetFloat("Speed", Mathf.Abs(_controller.Velocity.x) / MaxSpeed);
    }


    public void FinishLevel()
    {
        enabled = false;
        _controller.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
    public void Kill()
    {
        _controller.HandleCollisions = false;
        GetComponent<Collider2D>().enabled = false;

        IsDead = true;

        _controller.SetForce(new Vector2(0, 20));
        Health = 0;
		GameManager.Instance.resetMultiplier ();
    }

    public void RespawnAt(Transform spawnPoint)
    {
        if (!_isFacingRight)
            Flip();

        IsDead = false;
        GetComponent<Collider2D>().enabled = true;
        _controller.HandleCollisions = true;
        Health = MaxHealth;

        transform.position = spawnPoint.position;


    }

    public void TakeDamage(int damage, GameObject instigator)
    {

        if (PlayerHitSound != null)
            AudioSource.PlayClipAtPoint(PlayerHitSound, transform.position);
		if (damage != 0)
			FloatingText.Show(string.Format("-{0}!", damage), "DamageText", new FromWorldPointTextPositioner(Camera.main, transform.position, DamageTextLifeSpan, DamageTextSpeed));

		if (OuchEffect != null)
			Instantiate(OuchEffect, transform.position, transform.rotation);
        Health -= damage;

        if (Health <= 0)
            LevelManager.Instance.KillPlayer();
    }

    public void changeProjectile(Projectile projectile)
    {
        CurrentProjectile = projectile;
    }

    public void GiveHealth(int health, GameObject instagator)
    {
        if (PlayerHealthSound != null)
            AudioSource.PlayClipAtPoint(PlayerHealthSound, transform.position);

        FloatingText.Show(string.Format("+{0}!",health), "PlayerGotHealthText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 60f));

        Health = Mathf.Min(Health + Health, MaxHealth);
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _normalizedHorizontalSpeed = 1;
            if (!_isFacingRight)
                Flip();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _normalizedHorizontalSpeed = -1;
            if (_isFacingRight)
                Flip();
        }
        else
            _normalizedHorizontalSpeed = 0;

        if (_controller.CanJump && Input.GetKeyDown(KeyCode.Z))
        {
            _controller.Jump();
        }

        if (Input.GetKeyDown(KeyCode.X))
            FireProjectile();


    }

    private void FireProjectile()
    {

        if (_canFireIn > 0)
            return;

        if (FireProjectileEffect != null)
        {
            var effect = (GameObject) Instantiate(FireProjectileEffect, ProjectileFireLocation.position, ProjectileFireLocation.rotation);
            effect.transform.parent = transform;
        }



        var direction = _isFacingRight ? Vector2.right : -Vector2.right;

        var projectile = (Projectile)Instantiate(CurrentProjectile, ProjectileFireLocation.position, ProjectileFireLocation.rotation);
        projectile.Initialize(gameObject, direction, _controller.Velocity);


        _canFireIn = FireRate;
        if (PlayerShootSound != null)
            AudioSource.PlayClipAtPoint(PlayerShootSound, transform.position);

        Animator.SetTrigger("Fire");
    }



    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isFacingRight = transform.localScale.x > 0;
    }


}
