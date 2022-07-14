using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private GameObject bubbles;
	[SerializeField] private float rotationSpeed;

	private Status status;
	private new Rigidbody rigidbody;
	private Animator animator;

	private Vector3 lastInput;
	private bool isRushing = false;
	private bool canRush = true;
	private bool isInWater = true;
	private float currentRushTime = 0;

	[HideInInspector]
	public UnityEvent<float, float> onRushValueChanged = new UnityEvent<float, float>();

	private void Start()
	{
		status = GetComponent<Status>();
		rigidbody = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();

		currentRushTime = status.RushTime;
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
		//UpdateLimitY();
	}

	private void UpdateMove()
	{
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");
		lastInput = new Vector3(x, y, 0);

		rigidbody.velocity = lastInput.normalized * (!isRushing ? status.MoveSpeed : status.MoveSpeed + status.RushSpeed);

		if (!isInWater && lastInput.y > 0)
			transform.position = new Vector3(transform.position.x, 10, transform.position.z);
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
		float velocityX = rigidbody.velocity.x;

		if (velocityX != 0)
			transform.localScale = new Vector3(Mathf.Sign(velocityX), 1, 1);
	}

	private void UpdateAnimation()
	{
		animator.SetFloat("moveSpeed", Mathf.Abs(rigidbody.velocity.magnitude));
		animator.SetBool("isRushing", isRushing);
	}

	private void UpdateRush()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift) && !isRushing)
		{
			isRushing = true;
			Instantiate(bubbles, gameObject.transform.position, gameObject.transform.rotation);
		}

		if (isRushing)
		{
			if (IsRushTimeLeft())
				Rush();
			else
				isRushing = false;
		}

		if (!isRushing)
			RushCooldown();
	}

	private void Rush()
	{
		currentRushTime -= Time.deltaTime;
		ClampRushTime();
		UpdateRushUI();
	}

	private void RushCooldown()
	{
		currentRushTime += Time.deltaTime * (status.RushTime / status.RushCooltime);
		ClampRushTime();
		UpdateRushUI();
	}

	public bool IsRushTimeLeft()
	{
		if (currentRushTime <= 0)
			return false;
		return true;
	}

	private void UpdateRushUI()
	{
		onRushValueChanged.Invoke(status.RushTime, currentRushTime);
	}

	private void ClampRushTime()
	{
		currentRushTime = Mathf.Clamp(currentRushTime, 0, status.RushTime);
	}

	private void UpdateLimitY()
    {
		if (transform.position.y >= ScreenSettings.Instance.WaterLimitY)
		{
			ScreenSettings.Instance.ChangeScreen(ScreenType.air);
			isInWater = false;
		}
		else
		{
			ScreenSettings.Instance.ChangeScreen(ScreenType.water);
			isInWater = true;
		}
	}
}
