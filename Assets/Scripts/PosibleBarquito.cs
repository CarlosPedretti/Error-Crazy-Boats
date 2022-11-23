using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class PosibleBarquito : MonoBehaviour
{
	public Rigidbody rb;

	float verticalInput;
	float movementFactor;
	float horizontalInput;
	float steerFactor;
	float clampedSpeed;
    float clampedSteerSpeed;

    public float speed = 1.0f;
	public float steerSpeed = 1.0f;
    public float movementThresold = 10.0f;
	public float minSpeedLimit = 1f;
	public float maxSpeedLimit = 3f;
	public float minSpeedSteerLimit = 2f;
	public float maxSpeedSteerLimit = 3f;

    private InputActionAsset inputAsset;
	private InputActionMap Player;
	private InputAction move;



	public void OnMove(InputAction.CallbackContext context)
    {
		verticalInput = context.ReadValue<float>();

    }

	

	public void OnSteer(InputAction.CallbackContext context)
    {
		horizontalInput = context.ReadValue<float>();
	}
	


	private void Awake()
    {
		inputAsset = this.GetComponent<PlayerInput>().actions;
		Player = inputAsset.FindActionMap("Player");
    }

	private void OnEnable()
    {
		move = Player.FindAction("Move");
		Player.Enable();
    }

	private void OnDisable()
    {

    }

	void Start()
    {

	}

	void Update()
    {
		Movement();
		Steer();

	}

	void Movement()
    {



		verticalInput = Input.GetAxis("Vertical");
		clampedSpeed = Mathf.Clamp(speed, minSpeedLimit, maxSpeedLimit);
		movementFactor = Mathf.Lerp(movementFactor, verticalInput, Time.deltaTime / movementThresold);
		transform.Translate(0.0f, 0.0f, movementFactor * clampedSpeed);

	}

	void Steer()
	{

		horizontalInput = Input.GetAxis("Horizontal");
		clampedSteerSpeed = Mathf.Clamp(steerSpeed, minSpeedSteerLimit, maxSpeedSteerLimit);
		steerFactor = Mathf.Lerp(steerFactor, horizontalInput * verticalInput, Time.deltaTime / movementThresold);
		transform.Rotate(0.0f, steerFactor * clampedSteerSpeed, 0.0f);

	}
}