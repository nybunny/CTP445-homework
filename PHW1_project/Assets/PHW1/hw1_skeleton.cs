using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// Listens for touch events and performs an AR raycast from the screen touch point.
    /// AR raycasts will only hit detected trackables like feature points and planes.
    ///
    /// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
    /// and moved to the hit position.
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    public class hw1_skeleton : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]
        GameObject m_PlacedPrefab;

        /// <summary>
        /// The prefab to instantiate on touch.
        /// </summary>
        public GameObject placedPrefab
        {
            get { return m_PlacedPrefab; }
            set { m_PlacedPrefab = value; }
        }

        /// <summary>
        /// The object instantiated as a result of a successful raycast intersection with a plane.
        /// </summary>
        public GameObject spawnedObject { get; private set; }

        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
        }

        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
    #if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                var mousePosition = Input.mousePosition;
                touchPosition = new Vector2(mousePosition.x, mousePosition.y);
                return true;
            }
    #else
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }
    #endif

            touchPosition = default;
            return false;
        }

        void Update()
        {   
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = s_Hits[0].pose;
                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation); //Instantiate(T original, Vector3 position, Quaternion rotation)
                }
                else
                {
                    spawnedObject.transform.position = hitPose.position;
                }
            }
            /* 
            //1. Fill the 'if' statement to check if the touched position hit a trackable object(plane) through raycast
            //    [Function] m_RaycastManager.Raycast(...)
            //    [Variable] touchPosition, s_Hits, TrackableType.PlaneWithinPolygon
            if ()
            {
                //2. If raycast hit a trackable object, find the closest hit.
                //    [Tip] Raycast hits are sorted by distance, so the first one will be the closest hit.
                var hitPose = 

                //3. If there is no spawnedObject, create new object with assigned prefab and the touched position
                //    [Function] Instantiate(...)
                //    [Variable] spawnedObject, m_PlacedPrefab, hitPose
                if (spawnedObject == null)
                {
                }
                //4. If spawnedObject exists, translate the spawnedObject to the touched position
                else
                {                    
                }
            }
            */
        }

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;
    }
}