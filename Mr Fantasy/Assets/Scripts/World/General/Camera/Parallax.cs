using UnityEngine;

namespace World.General.Camera
{
    public class Parallax : MonoBehaviour
    {
        private float length, startPos;
        public GameObject cam;
        public SpriteRenderer SpriteRenderer;
        public float parallaxEffectX;
        public float parallaxEffectY;
        private float initialYPos;
        public bool lockVertical;

        private void Start()
        {
            startPos = transform.position.x;
            length = SpriteRenderer.bounds.size.x;
            initialYPos = transform.position.y;
        }

        private void Update()
        {
            float temp = cam.transform.position.x * (1 - parallaxEffectX);
            float xDist = cam.transform.position.x * parallaxEffectX;
            float yDist = cam.transform.position.y * parallaxEffectY;
            
            transform.position = new Vector3(startPos + xDist, initialYPos + yDist, transform.position.z);

            if (temp > startPos + length) startPos += length;
            else if (temp < startPos - length) startPos -= length;
        }
    }
}
