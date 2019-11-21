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
        public float yOffset;

        #endregion

        #region Custom Methods

          /** Configure and instantiate the new health pop up */
        private void InitializePopUp()
        {
            // Load the Arial font from the Unity Resources folder.
            Font arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

            // Create Canvas GameObject.
            healthPopUp = new GameObject {name = "Canvas"};
            healthPopUp.AddComponent<Canvas>();
            healthPopUp.AddComponent<CanvasScaler>();
            healthPopUp.AddComponent<GraphicRaycaster>();
            healthPopUp.AddComponent<HealthDestroyer>();

            // Get canvas from the GameObject.
            Canvas canvas = healthPopUp.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.sortingOrder = 50;

            // Create the Text GameObject.
            GameObject healthPopUpText = new GameObject();
            healthPopUpText.transform.parent = healthPopUp.transform;
            healthPopUpText.AddComponent<Text>();

            // Set Text component properties.
            textMesh = healthPopUpText.GetComponent<Text>();
            textMesh.font = arial;
            textMesh.fontSize = 10;
            textMesh.alignment = TextAnchor.MiddleCenter;

            RectTransform rectTransform = textMesh.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, 0, 0);
            rectTransform.sizeDelta = new Vector2(100, 100);

            // Set destroy timer.
            healthPopUp.GetComponent<HealthDestroyer>().destroyTime = .2f;
        }

        /** Set the color based on the variation received and instantiate the pop up above the hit object */
        public void ShowHealthVariation(float variation, Transform hitObject)
        {
            InitializePopUp();
            if (variation > 0)
            {
                textMesh.color = Color.green;
                textMesh.text = "+";
            }
               
            if (variation < 0)
            {
                textMesh.color = Color.red;
                textMesh.text = "-";
            }
                
            //textMesh.text = Mathf.Abs(variation).ToString(NumberFormatInfo.CurrentInfo);
            healthPopUp.transform.position = new Vector2(hitObject.position.x, hitObject.position.y + yOffset);
            healthPopUp.transform.localScale = new Vector2(0.3f, 0.3f);
        }

        #endregion
    }
}