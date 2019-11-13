using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace World.General.HealthManager
{
    /** Display above the hit object the increase or decrease value of health. */
    public class HealthVariationDisplayer : MonoBehaviour
    {
        #region Objects

        private GameObject canvasGO;
        public GameObject healthPopUp;
        private Text text;
        private Text textMesh;
        private MeshRenderer meshRenderer;

        #endregion

        #region Settings Parameters

        private float timer;
        private int num = 0;
        public float yOffset;

        #endregion

        /** Instantiates the new health pop up */
        private void InitializePopUp()
        {
            // Load the Arial font from the Unity Resources folder.
            var arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

            // Create Canvas GameObject.
            if (num == 1)
            {
                canvasGO = new GameObject {name = "Canvas"};
                canvasGO.AddComponent<Canvas>();
                canvasGO.AddComponent<CanvasScaler>();
                canvasGO.AddComponent<GraphicRaycaster>();
                canvasGO.AddComponent<HealthDestroyer>();

                // Get canvas from the GameObject.
                var canvas = canvasGO.GetComponent<Canvas>();
                canvas.renderMode = RenderMode.WorldSpace;
                canvas.sortingOrder = 50;

                // Create the Text GameObject.
                GameObject textGO = new GameObject();
                textGO.transform.parent = canvasGO.transform;
                textGO.AddComponent<Text>();

                // Set Text component properties.
                text = textGO.GetComponent<Text>();
                text.font = arial;
                text.fontSize = 10;
                text.alignment = TextAnchor.MiddleCenter;

                // Provide Text position and size using RectTransform.
                var rectTransform = text.GetComponent<RectTransform>();
                rectTransform.localPosition = new Vector3(0, 0, 0);
                rectTransform.sizeDelta = new Vector2(100, 100);
//                Debug.Log("Called Method InitializePopUp()");

                canvasGO.GetComponent<HealthDestroyer>().destroyTime = .2f;
            }
        }

        /** Set the color based on the variation received and instantiate the pop up above the hit object */
        public void ShowHealthVariation(float variation, Transform hitObject)
        {
          //  Debug.Log("Called Method ShowHealthVariation()");
            if (num == 0)
            {
                num++;
                if (num == 1)
                {
                    //num++;
                    InitializePopUp();
                }
                
                variation *= 10;
                if (variation > 0)
                    text.color = Color.green;
                if (variation < 0)
                    text.color = Color.red;
                text.text = Mathf.Abs(variation).ToString(NumberFormatInfo.CurrentInfo);
                canvasGO.transform.position = new Vector2(hitObject.position.x, hitObject.position.y + yOffset);
                canvasGO.transform.localScale = new Vector2(0.3f, 0.3f);
                StartCoroutine(Wait(canvasGO));
            }
        }
        
        

        IEnumerator Wait(GameObject destroyGameObject)
        {
            yield return new WaitForSeconds(.3f);
            Destroy(destroyGameObject);
            Debug.LogWarning("Destroyed GAmeboj");
            num = 0;
        }
    }
}