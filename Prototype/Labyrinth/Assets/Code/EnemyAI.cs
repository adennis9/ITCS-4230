using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public float Speed;
	public float TimeToWait = .5f;
	public float FireRate = 1f;
	public float FireWait = 1f;
	public Projectile Projectile;
	public float EnemySight = 10;


    private CharacterController2D _controller;
    private Vector2 _direction;
    private float _wait = .5f;
	private float _fire;
    public void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        _direction = new Vector2(-1, 0);


    }

    public void Update()
    {
        var rayCast = Physics2D.Raycast(transform.position, _direction, EnemySight, 1 << LayerMask.NameToLayer("player"));

        if ((_direction.x < 0 && _controller.State.IsCollidingLeft) || (_direction.x > 0 && _controller.State.IsCollidingRight))
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
