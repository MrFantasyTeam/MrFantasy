using System;
using UnityEngine;

namespace World.General.Camera
{
    public class ParallaxB : MonoBehaviour
    {
        public Transform[] backgrounds;
        private float[] parallaxScales;
        public float smoothing = 1;
        private Transform cam;
        private Vector3 previousCamPos;

        private void Awake()
        {
            if (UnityEngine.Camera.main != null) cam = UnityEngine.Camera.main.transform;
        }

        private void Start()
        {
            previousCamPos = cam.position;
            parallaxScales = new float[backgrounds.Length];

            for (int i = 0; i < backgrounds.Length; i++)
            {
                parallaxScales[i] = backgrounds[i].position.z * -1;
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
                float backgroundTargetPosX = backgrounds[i].position.x + parallax;
                
                Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

                backgrounds[i].position =
                    Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
            }

            previousCamPos = cam.position;
        }
    }
}
