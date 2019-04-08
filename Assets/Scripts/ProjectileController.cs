using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
	private GameObject target;
	private SpriteRenderer spriteRenderer;
	private Rigidbody2D rigidbody2D;
	private NoteMasterController noteMaster;
	private AudioSource audioSource;
	private float a;
	private float initialSpeed;
	private float speed;

	private void Awake()
	{
		// staffMaster = transform.parent.gameObject.GetComponent<StaffMasterController>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		noteMaster = GameObject.Find("NoteMaster").GetComponent<NoteMasterController>();
		a = 0f;
		initialSpeed = 0f;
		speed = 0f;
	}

	public void Initialize(GameObject tar, GameObject clef, float accel, float initSpeed)
	{
		target = tar;
		a = accel;
		initialSpeed = initSpeed;
		speed = initialSpeed;
		Color parentColor = clef.GetComponent<SpriteRenderer>().color;
		spriteRenderer.color = parentColor;
	}

	private void FixedUpdate()
	{
		Vector3 direction = target.transform.position - transform.position;
		direction = direction.normalized;
		speed = Mathf.Max(10f, speed + a * Time.fixedDeltaTime);
		rigidbody2D.MovePosition(rigidbody2D.position + (Vector2)(direction * speed * Time.fixedDeltaTime));
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject == target)
		{
			noteMaster.EventNoteHit(other.gameObject, gameObject);
			Destroy(gameObject);
		}
	}
}
