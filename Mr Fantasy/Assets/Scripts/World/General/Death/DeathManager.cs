using MainScripts;
using UnityEngine;

namespace World.General.Death
{
    public class DeathManager : MonoBehaviour
    {
        #region Objects

        public Animator anim;
        private GameObject mainCamera;
        public LevelManager levelManager;
        private AnimatorStateInfo animatorStateInfo;

        #endregion

        #region Settings Parameters

        private static readonly int Death = Animator.StringToHash("Death");
        private const string MainCameraTag = "MainCamera";
        private Vector3 initialPos;
        public string deathAnimName;
        public int levelNumber;

        #endregion

        #region Boolean Values

        public bool startDeathAnim;
        private bool triggeredDeathAnim;
        public bool resetPosition;
        public bool destroy;

        #endregion

        #region Default Methods

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            anim = GetComponent<Animator>();
            mainCamera = GameObject.FindWithTag(MainCameraTag);
            levelManager = mainCamera.GetComponent<LevelManager>();
            initialPos = transform.position;
            enabled = false;
        }

        private void FixedUpdate()
        {
            if (startDeathAnim) StartDeathAnim(levelNumber, deathAnimName);
            if (resetPosition) return;
            Vector3 cameraPos = levelManager.transform.position;
            transform.position = new Vector3(cameraPos.x, cameraPos.y, 0);

        }

        #endregion

        #region Custom Methods

        private void StartDeathAnim(int level, string animName)
        {
            if (!triggeredDeathAnim)
            {
                triggeredDeathAnim = true;
                anim.SetTrigger(Death);
                StartCoroutine(levelManager.ReloadLevelAsync(level));
            }
            animatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (!animatorStateInfo.IsName(animName)) return;
            if (!destroy && levelManager.progress >= .9f)
            {
                DelayDestroyDeathManager();
            }
            if (animatorStateInfo.normalizedTime < .75f) return;
            if (levelManager.progress < .9f)
            {
                anim.enabled = false;
                return;
            }
            if (resetPosition) return;
            ResetPosition();
        }
        
        #region Secondary Methods

        private void ResetPosition()
        {
            anim.enabled = true;
            resetPosition = true;
            transform.position = initialPos;
            CreateSecondCamera();
            levelManager.dead = true;
        }
        private void DelayDestroyDeathManager()
        {
            destroy = true;
            float animElapsedTime = animatorStateInfo.length * animatorStateInfo.normalizedTime;
            float length = animatorStateInfo.length;
            float timeLeft = length - animElapsedTime;
            Destroy(gameObject, timeLeft *.95f);
        }

        private void CreateSecondCamera()
        {
            Vector3 cameraPos = new Vector3(initialPos.x, initialPos.y, mainCamera.transform.position.z);
            GameObject secondCamera = new GameObject();
            secondCamera.transform.SetParent(transform);
            secondCamera.transform.position = cameraPos;
            secondCamera.AddComponent<UnityEngine.Camera>();
            UnityEngine.Camera secondCameraComp = secondCamera.GetComponent<UnityEngine.Camera>();
            UnityEngine.Camera tempCameraSettings = mainCamera.GetComponent<UnityEngine.Camera>();
            secondCameraComp.orthographic = tempCameraSettings.orthographic;
            secondCameraComp.orthographicSize = tempCameraSettings.orthographicSize;
            secondCameraComp.cullingMask = tempCameraSettings.cullingMask;
            secondCameraComp.clearFlags = tempCameraSettings.clearFlags;
            mainCamera.SetActive(false);
        }

        #endregion

        #endregion
        }
}
