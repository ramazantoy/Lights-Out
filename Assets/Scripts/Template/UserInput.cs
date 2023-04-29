
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace LeonBrave
{
	public class UserInput : MonoBehaviour
	{
		public static UserInput Instance;

		void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else if (Instance != this)
			{
				Destroy(gameObject);
			}
		}

		public RaycastHit Raycast(Transform raycastTransform, Vector3 raycastOffset, float distance = Mathf.Infinity)
		{
			Vector3 raycastOrigin = raycastTransform.position + raycastTransform.TransformDirection(raycastOffset);

			RaycastHit hit;

			Physics.Raycast(raycastOrigin, raycastTransform.forward, out hit, distance);

			return hit;
		}

		public RaycastHit Raycast(Transform raycastTransform, Vector3 raycastOffset, LayerMask layerMask,
			float distance = Mathf.Infinity)
		{
			Vector3 raycastOrigin = raycastTransform.position + raycastTransform.TransformDirection(raycastOffset);

			RaycastHit hit;

			Physics.Raycast(raycastOrigin, raycastTransform.forward, out hit, distance, layerMask);

			return hit;
		}


		public RaycastHit Raycast()
		{
			Ray ray = Camera.main.ScreenPointToRay(TouchPos);
			RaycastHit hit;
			Physics.Raycast(ray,out hit,Mathf.Infinity);
			return hit;
		}


		public RaycastHit Raycast(LayerMask targetLayerMask)
		{
			Ray ray = Camera.main.ScreenPointToRay(TouchPos);
			RaycastHit hit;
			Physics.Raycast(ray, out hit,Mathf.Infinity,layerMask:targetLayerMask);
			return hit;
		}


		public delegate void MovementEventHandler(Vector3 movement);


		public event MovementEventHandler MovementEvent;
		public delegate void MovementFromFirstPosEventHandler(Vector3 movement);


		public event MovementFromFirstPosEventHandler MovementEventFromFirstPos;

		private void Update()
		{
			Movement(); //Check Movements


			Touch(); //Check Touch Types
			
			
			Swipe(); //Swipe Checks
		}

		private Vector3 _startingTouchPos;

		// Movement threshold
		public float movementThreshold = 0.1f;
		private void Movement()
		{
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);

				MovementEvent?.Invoke(touch.deltaPosition.normalized);
				if (touch.phase == TouchPhase.Began)
				{
					_startingTouchPos = touch.position;
				}
				else if (touch.phase == TouchPhase.Moved)
				{
					Vector3 movementVector = touch.deltaPosition.normalized;

					if (touch.deltaPosition.magnitude <= 0.1f)
					{
						MovementEventFromFirstPos?.Invoke(((Vector3)touch.position-_startingTouchPos));
					}
					else if (movementVector.magnitude > movementThreshold)
					{
				
						MovementEventFromFirstPos?.Invoke(movementVector);
					}
				}
	
			}
			else if (Input.GetKey(KeyCode.Mouse0))
			{
		
				Vector3 movementVector = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
				MovementEvent?.Invoke(movementVector);
				
				if (movementVector.magnitude <= 0.1f)
				{
					MovementEventFromFirstPos?.Invoke(Input.mousePosition - _startingTouchPos);
				}
				else if (movementVector.magnitude > movementThreshold)
				{
					MovementEventFromFirstPos?.Invoke(movementVector);
				}
			}
		}

		private Vector3 _touchPos;

		public Vector3 TouchPos
		{
			get
			{
				return _touchPos;
			}
		}


		private void Touch()
		{
			if (GetFingerState() != TouchType.None)
			{
				TouchEvent?.Invoke(GetFingerState());
			}

	
		}

		private TouchType GetFingerState()
		{
			if (Input.touchCount > 0)
			{

				Touch touch = Input.GetTouch(0);

				switch (touch.phase)
				{
					case TouchPhase.Began:
						_touchPos = Input.GetTouch(0).position;
						return TouchType.Down;
					case TouchPhase.Moved:
						return TouchType.HoldDown;
					case TouchPhase.Stationary:
						return TouchType.HoldDown;
					case TouchPhase.Ended:
						_touchPos = Vector3.zero;
						return TouchType.Up;
					default:
						return TouchType.None;
				}
			} 
			
			if (Input.GetMouseButtonDown(0)) {
			 	_touchPos = Input.mousePosition;
				return TouchType.Down;
			}
			if (Input.GetMouseButton(0))
			{
				_touchPos = Input.mousePosition;
				return TouchType.HoldDown;
			}
			    
			if (Input.GetMouseButtonUp(0))
			{
				_touchPos = Vector3.zero;
				return TouchType.Up;
			}
			    
			return TouchType.None;
		}
	


		public delegate void TouchEventHandler(TouchType touchType);

		public event TouchEventHandler TouchEvent;

		public enum TouchType
		{
			None,
			Down,
			HoldDown,
			Up
		}
				
		public enum SwipeType
		{
			None,
			Left,
			Right,
			Up,
			Down
		}
		
		private Vector2 startPos;
		private Vector2 endPos;
		private Vector2 direction;
		
		public delegate void SwipeTypeEventHandler(SwipeType swipeType);

		public event SwipeTypeEventHandler SwipeTypeEvent;


		private void Swipe()
		{
			SwipeTypeEvent?.Invoke(GetSwipeDirection());
		}
		[SerializeField]
		private float swipeThreshold = 50f;
		   private SwipeType GetSwipeDirection()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                endPos = touch.position;
                direction = endPos - startPos;

                if (Mathf.Abs(direction.x) > swipeThreshold && Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    if (direction.x > 0)
                    {
                        return SwipeType.Right;
                    }
                    else
                    {
                        return SwipeType.Left;
                    }
                }
                else if (Mathf.Abs(direction.y) > swipeThreshold && Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
                {
                    if (direction.y > 0)
                    {
                        return SwipeType.Up;
                    }
                    else
                    {
                        return SwipeType.Down;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            direction = endPos - startPos;

            if (Mathf.Abs(direction.x) > swipeThreshold && Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    return SwipeType.Right;
                }
                else
                {
                    return SwipeType.Left;
                }
            }
            else if (Mathf.Abs(direction.y) > swipeThreshold && Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
            {
                if (direction.y > 0)
                {
                    return SwipeType.Up;
                }
                else
                {
                    return SwipeType.Down;
                }
            }
        }

        return SwipeType.None;
    }


	}
}
