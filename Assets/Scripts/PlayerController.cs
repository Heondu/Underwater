using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Bubbles bubbles;
	[SerializeField] private Transform bubblesPoint;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private Animator[] animators;
	[SerializeField] private GameObject[] playerCharacters;

	private Status status;
	private new Rigidbody rigidbody;

	private Vector3 lastInput;
	private bool isRushing = false;
	private float currentRushTime = 0;

	[HideInInspector]
	public UnityEvent<float, float> onRushValueChanged = new UnityEvent<float, float>();

	private void Awake()
	{
		status = GetComponent<Status>();
		rigidbody = GetComponent<Rigidbody>();
		PieceOfLightManager.Instance.onPieceOfLightAdded.AddListener(UpdatePlayerCharacter);

		currentRushTime = status.RushTime;
	}

	private void Update()
	{
		UpdateRush();
		UpdateFlip();
		UpdateAnimation();

		BlowBubbles();
	}

    private void FixedUpdate()
    {
		UpdateMove();
		UpdateRotation();
	}

	private void UpdateMove()
	{
		float x = PlayerInput.Horizontal;
		float y = PlayerInput.Vertical;
		lastInput = new Vector3(x, y, 0);
		Vector3 direction = lastInput;

		//if (followCameraForward)
		//{
		//	Vector3 up = Camera.main.transform.up;
		//	Vector3 right = Camera.main.transform.right;
		//	direction = right * lastInput.x + up * lastInput.y;
		//}

		if (transform.position.y >= 5 && !Physics.Raycast(transform.position, Vector3.down, 1f, groundLayer) && lastInput.y > 0)
			direction.y = 0;

		rigidbody.velocity = direction.normalized * (!isRushing ? status.MoveSpeed : status.MoveSpeed + status.RushSpeed);
	}

	private void UpdateRotation()
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
		//animator.SetFloat("moveSpeed", Mathf.Abs(rigidbody.velocity.magnitude));
		if (PieceOfLightManager.Instance.PieceOfLightNum > 0)
			animators[PieceOfLightManager.Instance.PieceOfLightNum].SetBool("isRushing", isRushing);
	}

	private void UpdateRush()
	{
		if (PlayerInput.RushDown && !isRushing)
		{
			isRushing = true;
			Bubbles clone = Instantiate(bubbles, gameObject.transform.position, gameObject.transform.rotation);
			clone.SetDirection(Vector3.zero);
		}
		else if (PlayerInput.RushUp && isRushing)
        {
			isRushing = false;
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

	private void UpdatePlayerCharacter(int index)
    {
		for (int i = 0; i < playerCharacters.Length; i++)
        {
			if (i == index)
				playerCharacters[i].SetActive(true);
			else
				playerCharacters[i].SetActive(false);
        }
    }

	private void ClampRushTime()
	{
		currentRushTime = Mathf.Clamp(currentRushTime, 0, status.RushTime);
	}

	private void BlowBubbles()
    {
		if (PlayerInput.BubbleDown)
        {
			Bubbles clone = Instantiate(bubbles, bubblesPoint.position, gameObject.transform.rotation);
			clone.SetDirection(GetDirection());
		}
    }

	private Vector3 GetDirection()
    {
		Vector3 direction = lastInput;
		if (direction == Vector3.zero)
			direction = transform.localScale.x >= 0 ? Vector3.right : Vector3.left;
		return direction.normalized;
	}
}
