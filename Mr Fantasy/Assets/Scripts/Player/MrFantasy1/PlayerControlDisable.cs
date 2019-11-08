using UnityEngine;

/** Script to control the scene swithcing at the end of the first level.
 * Disables the player movements, takes control of camera and switches the scene.
 */
public class PlayerControlDisable : MonoBehaviour
{
    #region Objects

    public Camera firstCameraController;
    public Camera secondCameraController;
    public CameraMove cameraMove;
    public GameObject player;
    public GameObject transformationAnim;
    public GameObject animTransformationPosition;
    public GameObject showScenePositionIncrease;
    public GameObject showScenePositionDecrease;
    public GameObject level;
    public GameObject background;
    public GameObject chamber;
    public GameObject mrFantasyPesce;
    public LevelManager levelManager;
    public PlayerMovement playerMovement;
    public Transform testObject;
    public Transform playerPosition;

    #endregion

    #region Settings Parameters

    public Vector2 reachVector;
    public float speed;
    public float timer;
    public float time;
    public float time2;
    public float time3;
    public float time4;
    public float zoomSpeed;
    public float switchCameraTime;
    public float destroyAnimTime;
    public int counter = 0;
    public int counter1 = 0;
    public float x, y, z;
    private float x1, y1, x2, y2, x3, y3;
    public float cameraOriginalSize;

    #endregion

    #region Boolean Values

    public bool decreasing;
    public bool increasing;
    public bool stopped;
    public bool startReachingPosition;
    public bool disabled;
    public bool stopMoving;
    public bool focus;
    public bool switchCamera;
    public bool keepZooming;
    public bool velocitySetted;
    public bool go;
    
    #endregion

    #region Default Methods

    private void Start()
    {
        cameraMove = firstCameraController.GetComponent<CameraMove>();
        levelManager = new LevelManager();
    }

    private void Update()
    {   
        if(startReachingPosition)
        {
            x = x1;
            y = y1;
            playerMovement.ReachPosition(animTransformationPosition);
            startReachingPosition = false;
        }
        if(focus)
        {
            timer += Time.deltaTime;
            FocusOnPlayer();
        }
        if(go)
        {
            timer += Time.deltaTime;
            
            ShowScene();
        }
        if(switchCamera)
        {
            timer += Time.deltaTime;
            background.SetActive(false);
            if (timer > switchCameraTime && timer < destroyAnimTime)
            {
                Destroy(firstCameraController.gameObject);
                secondCameraController.GetComponent<Camera>().enabled = true;
                secondCameraController.GetComponent<AudioListener>().enabled = true;
            }       
            else if(timer > destroyAnimTime)
            {
                transformationAnim.SetActive(false);
                chamber.GetComponent<SpriteRenderer>().enabled = true;
                mrFantasyPesce.GetComponent<SpriteRenderer>().enabled = true;
                go = true;
                Debug.Log("disabled animation");
                switchCamera = false;
            }
        }
    }

    #endregion

    #region Custom Methods 

    /**
     * If the object entering the trigger is the player, take its coordinates
     * and set correct boolean values to start related methods.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {   
            if(counter==0)
            {
                player = collision.gameObject;
                counter++;
                playerMovement = collision.GetComponent<PlayerMovement>();
                playerMovement.GetComponent<Rigidbody2D>().gravityScale = 0;
                x1 = playerMovement.transform.position.x;
                y1 = playerMovement.transform.position.y;
                x2 = this.transform.position.x;
                y2 = this.transform.position.y;
                x3 = firstCameraController.transform.position.x;
                y3 = firstCameraController.transform.position.y;
                focus = true;
                startReachingPosition = true;               
            }
        }
    }

    /**
     * Focus the camera on the player and keep moving till the correct camera size.
     */
    public void FocusOnPlayer()
    {
        if (timer > time4)
        {
            timer = 0;
            timer += Time.deltaTime;
            decreasing = true;
            switchCamera = true;
            focus = false;            
        }
        else
        {
            cameraMove.speed = 0;
            firstCameraController.transform.position = Vector3.MoveTowards(firstCameraController.transform.position, transformationAnim.transform.position, 50 * Time.deltaTime);
            if (!keepZooming)
            {
<<<<<<< Updated upstream
                firstCameraController.orthographicSize += -speed * zoomSpeed * Time.deltaTime;
=======
                cameraController.orthographicSize = Mathf.SmoothStep(cameraController.orthographicSize, 1, Time.deltaTime*zoomSpeed);
                //Vector3.Slerp
>>>>>>> Stashed changes
            }
            if (timer > 1)
            {
                
            }

//            if (timer > 0.35)
//            {
//                keepZooming = true;             
//                player.SetActive(false);
//                transformationAnim.GetComponent<SpriteRenderer>().enabled = true;
//                transformationAnim.GetComponent<Animator>().enabled = true;
//                level.SetActive(false);
//            }
            if (cameraController.orthographicSize < 1.1f)
            {
                keepZooming = true;             
                player.SetActive(false);
                transformationAnim.GetComponent<SpriteRenderer>().enabled = true;
                transformationAnim.GetComponent<Animator>().enabled = true;
                level.SetActive(false);
            }
        }
    }

    /**
     * Show the second level initial scene. Zoom out till a certain point, then zoom in focusing on the player.
     */
    public void ShowScene()
    {
        if(counter1 == 0)
        {
            timer = 0;
            counter1++;
            cameraOriginalSize = secondCameraController.orthographicSize;
        }

        if (timer < time)
        {
            secondCameraController.transform.position = Vector3.MoveTowards(secondCameraController.transform.position, showScenePositionIncrease.transform.position, 1.9f*speed * Time.deltaTime);
            
            secondCameraController.orthographicSize += speed * Time.deltaTime;
            increasing = true;
        }
        else if (timer > time && timer < time2)
        {
            increasing = false;
            secondCameraController.orthographicSize = secondCameraController.orthographicSize;
        }
        else if(timer > time2 && timer < time3)
        {
            secondCameraController.transform.position = Vector3.MoveTowards(secondCameraController.transform.position, showScenePositionDecrease.transform.position, 5.7f * speed * Time.deltaTime);
            secondCameraController.orthographicSize += -3 * speed * Time.deltaTime;
            decreasing = true;
        }
        else if (timer > time3)
        {
            secondCameraController.orthographicSize = cameraOriginalSize;
            decreasing = false;
            stopped = true;
            StartCoroutine(levelManager.LoadAsync(3));
        }
    }

    #endregion
}
