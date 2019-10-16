using UnityEngine;

public class PlayerControlDisable : MonoBehaviour
{
    public Camera cameraController;
    public Camera camera2Controller;
    public CameraMove cameraMove;
    public GameObject player;
    public LevelManager levelManager;
    public PlayerMovement playerMovement;
    public Transform testObject;
    public Transform playerPosition;
    public GameObject transformationAnim;
    public GameObject animTransformationPosition;
    public GameObject showScenePositionIncrease;
    public GameObject showScenePositionDecrease;
    public GameObject level;
    public GameObject background;
    public GameObject chamber;
    public GameObject mrFantasyPesce;
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
    float x1, y1, x2, y2, x3, y3;

    public float x, y, z;
    public bool go;
    public float cameraOriginalSize;
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

    private void Start()
    {
        cameraMove = cameraController.GetComponent<CameraMove>();
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
                
                Destroy(cameraController.gameObject);
                camera2Controller.GetComponent<Camera>().enabled = true;
                camera2Controller.GetComponent<AudioListener>().enabled = true;
                              
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
                x3 = cameraController.transform.position.x;
                y3 = cameraController.transform.position.y;
                focus = true;
                startReachingPosition = true;               
            }
        }
    }

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
            cameraController.transform.position = Vector3.MoveTowards(cameraController.transform.position, transformationAnim.transform.position, 50 * Time.deltaTime);
            if (!keepZooming)
            {
                cameraController.orthographicSize += -speed * zoomSpeed * Time.deltaTime;
            }
            if (timer > 1)
            {
                
            }

            if (timer > 0.35)
            {
                keepZooming = true;             
                player.SetActive(false);
                transformationAnim.GetComponent<SpriteRenderer>().enabled = true;
                transformationAnim.GetComponent<Animator>().enabled = true;
                level.SetActive(false);
            }
        }
    }

    public void ShowScene()
    {
        
        if(counter1 == 0)
        {
            timer = 0;
            counter1++;
            cameraOriginalSize = camera2Controller.orthographicSize;
        }

        if (timer < time)
        {
            camera2Controller.transform.position = Vector3.MoveTowards(camera2Controller.transform.position, showScenePositionIncrease.transform.position, 1.9f*speed * Time.deltaTime);
            
            camera2Controller.orthographicSize += speed * Time.deltaTime;
            increasing = true;
        }
        else if (timer > time && timer < time2)
        {
            increasing = false;
            camera2Controller.orthographicSize = camera2Controller.orthographicSize;
        }
        else if(timer > time2 && timer < time3)
        {
            camera2Controller.transform.position = Vector3.MoveTowards(camera2Controller.transform.position, showScenePositionDecrease.transform.position, 5.7f * speed * Time.deltaTime);
            camera2Controller.orthographicSize += -3 * speed * Time.deltaTime;
            decreasing = true;
        }
        else if (timer > time3)
        {
            camera2Controller.orthographicSize = cameraOriginalSize;
            decreasing = false;
            stopped = true;
            StartCoroutine(levelManager.LoadAsync(3));
        }
    }

    
}
