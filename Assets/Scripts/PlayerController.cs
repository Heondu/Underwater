using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private float rushSpeed;
	private bool isRushing = false;
	private float speedMod;
	[SerializeField]
	private GameObject bubbles;
	[SerializeField]
	private float rotationSpeed;

	private new Rigidbody2D rigidbody2D;
	private Animator animator;

	private Vector3 lastInput;

	private void Start()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		UpdateRush();
		UpdateFlip();
		UpdateAnimation();
	}

    private void FixedUpdate()
    {
		UpdateMove();
		UpdateRotate();
	}

	private void UpdateMove()
	{
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");
		lastInput = new Vector3(x, y, 0);

		rigidbody2D.velocity = lastInput * (moveSpeed + speedMod);
	}

	private void UpdateRotate()
    {
		if (lastInput == Vector3.zero)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.zero), Time.deltaTime * rotationSpeed);
		}
		else
		{
			Vector3 direction = transform.localScale.x >= 0 ? lastInput : lastInput * -1;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
		}
    }

	private void UpdateFlip()
    {
		float velocityX = rigidbody2D.velocity.x;

		if (velocityX != 0)
			transform.localScale = new Vector3(Mathf.Sign(velocityX), 1, 1);
	}

	private void UpdateAnimation()
	{
		animator.SetFloat("moveSpeed", Mathf.Abs(rigidbody2D.velocity.magnitude));
		animator.SetBool("isRushing", isRushing);
	}

	private void UpdateRush()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift) && !isRushing)
		{
			isRushing = true;
			speedMod = rushSpeed;
			Instantiate(bubbles, gameObject.transform.position, gameObject.transform.rotation);
		}
		else if (Input.GetKeyUp(KeyCode.LeftShift) && isRushing)
        {
			isRushing = false;
			speedMod = 0;
        }
	}
}
