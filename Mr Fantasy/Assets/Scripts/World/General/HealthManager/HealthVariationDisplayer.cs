using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace World.General.HealthManager
{
    /** Display above the hit object the increase or decrease value of health. */
    public class HealthVariationDisplayer : MonoBehaviour
    {
        #region Objects

        private GameObject healthPopUp;
        private Text textMesh;
        private MeshRenderer meshRenderer;

        #endregion

        #region Settings Parameters

        private float timer;
        private const float DestroyTime = 1f;
        public float yOffset;

        #endregion

        /** Instantiates the new health pop up */
        private void InitializePopUp()
        {
            healthPopUp = new GameObject();
            textMesh = healthPopUp.AddComponent<Text>();
            textMesh.fontSize = 100;
            Debug.Log("Called Method InitializePopUp()");
        }

        /** Set the color based on the variation received and instantiate the pop up above the hit object */
        public void ShowHealthVariation(float variation, Transform hitObject)
        {
            Debug.Log("Called Method ShowHealthVariation()");
            InitializePopUp();
            if (variation > 0)
                textMesh.color = Color.green;
            if (variation < 0)
                textMesh.color = Color.red;
            textMesh.text = Mathf.Abs(variation).ToString(NumberFormatInfo.CurrentInfo);
            healthPopUp.transform.position = new Vector2(hitObject.position.x, hitObject.position.y + yOffset);
        }

        private void Update()
        {
//            timer += Time.deltaTime;
//            if (timer >= DestroyTime)
//                Destroy(gameObject);
        }
    }
}