using UnityEngine;

/** Manage platform vertical movement and behaviour. **/ 
namespace World.Lv1.Background.Platforms
{
	public class PlatformVerticalMove : MonoBehaviour 
	{
		#region Objects 

		Rigidbody2D rb;
	
		#endregion

		#region Settings Parameters

		private float time0;
		public float time1;
		public float time2;
		private float time3;
		private float time4;
		private float speed;
		private int times;
		private float tempSpeed;
		private float acc;
		private float timeDelta = 1.5f;

		#endregion

		#region Boolean Parameters

		private bool hasRigidBody;
		private bool started;

		#endregion

		#region Default Methods

		// Use this for initialization
		void Start () 
		{
			rb = GetComponent<Rigidbody2D> ();
			if (rb != null)
			{
				hasRigidBody = true;
			}
		
		}
	
		// Update is called once per frame
		void Update () 
		{
			time0 += Time.deltaTime;
			if (hasRigidBody)
			{
				if (time0 >= time1) 
				{
					rb.velocity = new Vector2 (0, speed);
				}

				if (!(time0 >= time2)) return;
				rb.velocity = new Vector2 (0, -speed);
				time0 = 0;
			}
			else
			{
				if (!started)
				{
					while (Random.Range(0, 1000) != 495)
					{
						return;
					}

					started = true;
					speed = Random.Range(5, 10);
					time1 = timeDelta;
					time2 = timeDelta * 2;
					time3 = timeDelta * 3;
					time4 = timeDelta * 4;
					acc = speed / (1.5f);
				}
				if (time0 <= time1)
				{
					if (tempSpeed < speed) tempSpeed += acc * Time.deltaTime;
					else tempSpeed = speed;
				} else if (time0 > time1 && time0 <= time2)
				{
					if (tempSpeed > 0) tempSpeed += -acc * Time.deltaTime;
					else tempSpeed = 0; 
				}else if (time0 > time2 && time0 <= time3)
				{
					if (tempSpeed > -speed) tempSpeed += -acc * Time.deltaTime;
					else tempSpeed = -speed;
				}else if (time0 > time3 && time0 <= time4)
				{
					if (tempSpeed < 0) tempSpeed += acc * Time.deltaTime;
					else tempSpeed = 0;
				} else time0 = 0;

				Vector2 transformPosition = transform.position;
				transformPosition = new Vector2(transformPosition.x, transformPosition.y + tempSpeed * Time.deltaTime);
				transform.position = transformPosition;
			}
		
		}

		#endregion

		#region Custom Methods

		/** Destroy this object on trigger enter with "Roof". **/
		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Roof")) 
			{
				Destroy (gameObject);
			}
		}
	
		#endregion
	}
}
