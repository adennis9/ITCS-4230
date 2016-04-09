using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyAI : MonoBehaviour, ITakeDamage
{

    public float Speed = 5f;
	public float TimeToWait = .5f;
	public float EnemySight = 10;
	public int PointsToGivePlayer = 1000;
	public int HitsToKill = 3;
	public enum weakness
	{
		Waterball,
		Fireball,
		Tornado,
		Earthfist

	}
	//public float FireRate = 1f;
	//public float FireWait = 1f;
	public weakness WeakAgainst;
	public Projectile Projectile;
	public Projectile WeaponToGive;
	public GameObject DestroyedEffect;
	public AudioClip DestroySound;



    private CharacterController2D _controller;
    private Vector2 _direction;
	private float _wait = .5f;
	private float _fire;
    

	public void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        _direction = new Vector2(-1, 0);
		//Debug.Log (WeakAgainst + "(Clone)");


    }
    public void Update()
    {
        var rayCast = Physics2D.Raycast(transform.position, _direction, EnemySight, 1 << LayerMask.NameToLayer("player"));
		var behindRayCast = Physics2D.Raycast(transform.position, -_direction, (EnemySight / 2), 1 << LayerMask.NameToLayer("player"));
		if ((_direction.x < 0 && _controller.State.IsCollidingLeft) || (_direction.x > 0 && _controller.State.IsCollidingRight) || behindRayCast)
        {
            _direction = -_direction;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        if (!rayCast)
        {
            _controller.SetHorizontalForce(_direction.x * Speed);
            return;
        }

        //_controller.SetHorizontalForce(0f);
		
		//Debug.Log ("I see you");
		//_fire = FireRate;
		if ((_wait -= Time.deltaTime) > 0)
            return;

        
		if (_controller.CanJump)
            _controller.Jump();

		StartCoroutine (Shoot ());
		//StartCoroutine (Shoot ());
		//StartCoroutine (Shoot ());
		//var projectile = (Projectile)Instantiate(Projectile, transform.position, (transform.rotation + 90));
		//projectile.Initialize(gameObject, _direction, _controller.Velocity);
        
		//Shoot ();
		_wait = TimeToWait;


     }
	public void TakeDamage(int damage, GameObject instigator)
	{
		var projectile = instigator.GetComponent<Projectile>();
		var owner = projectile.Owner.GetComponent<Player>();

		if (projectile.name != WeakAgainst + "(Clone)")
			return;


		if (PointsToGivePlayer != 0)
		{
			if (projectile != null && owner != null)
			{
				HitsToKill--;
				if (HitsToKill > 0)
					return;
				GameManager.Instance.AddPoints(PointsToGivePlayer * (GameManager.Instance.getMultiplier()));
				if (PointsToGivePlayer != 0)
					FloatingText.Show(string.Format("+{0}!", (PointsToGivePlayer * GameManager.Instance.getMultiplier())), "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50));

				if (DestroyedEffect != null)
					Instantiate(DestroyedEffect, transform.position, transform.rotation);
				if (DestroySound != null)
					AudioSource.PlayClipAtPoint(DestroySound, transform.position);
				gameObject.SetActive(false);
				GameManager.Instance.increaseMultiplier ();
				Debug.Log ("Total Points: " + GameManager.Instance.Points);
				owner.changeProjectile (WeaponToGive);
			
			}
		}

	}

	IEnumerator Shoot()
	{

		yield return new WaitForSeconds(.25f);
		var projectile = (Projectile)Instantiate(Projectile, transform.position, transform.rotation);
		projectile.Initialize(gameObject, _direction, _controller.Velocity);

		yield return new WaitForSeconds (.1f);
		var projectile2 = (Projectile)Instantiate(Projectile, transform.position, transform.rotation);
		projectile2.Initialize(gameObject, _direction, _controller.Velocity);

		yield return new WaitForSeconds (.1f);
		var projectile3 = (Projectile)Instantiate(Projectile, transform.position, transform.rotation);
		projectile3.Initialize(gameObject, _direction, _controller.Velocity);
	}
		

}
