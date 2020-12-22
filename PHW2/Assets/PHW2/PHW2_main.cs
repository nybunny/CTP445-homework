using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// Moves the ARSessionOrigin in such a way that it makes the given content appear to be
    /// at a given location acquired via a raycast.
    /// </summary>
    [RequireComponent(typeof(ARSessionOrigin))]
    [RequireComponent(typeof(ARRaycastManager))]
    public class PHW2_main : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("A transform which should be made to appear to be at the touch point.")]
        Transform m_Content;

        /// <summary>
        /// A transform which should be made to appear to be at the touch point.
        /// </summary>
        public Transform content
        {
            get { return m_Content; }
            set { m_Content = value; }
        }

        [SerializeField]
        [Tooltip("The rotation the content should appear to have.")]
        Quaternion m_Rotation;
        bool flag_smoothing;
        /// Add required variables
        float m_Scale;
        Vector3 newScale;
        float moveSpeed = 5f;
    
        public Quaternion rotation
        {
            get { return m_Rotation; }
            set
            {
                m_Rotation = value;
                if (m_SessionOrigin != null)
                    m_SessionOrigin.MakeContentAppearAt(content, content.transform.position, m_Rotation);
            }
        }

        //////////////////////////// TO DO #1 ///////////////////////////
        public float scale
        {
           get { return m_Scale; }
           set
            {
                m_Scale = value;
                if (m_SessionOrigin != null)
                {
                    newScale.x = m_Scale; newScale.y = m_Scale; newScale.z = m_Scale;
                    content.transform.localScale = newScale;
                    m_SessionOrigin.MakeContentAppearAt(content, content.transform.position);
                }
                //Debug.Log("scale=" + m_Scale);
            }
        }
        /////////////////////////////////////////////////////////////////


        //////////////////////////// TO DO #2 ///////////////////////////
        /// Implement function to toggle boolean variable 'flag_smoothing'
        
        public bool toggle_flag
        {
            get { return flag_smoothing; }
            set
            {
                flag_smoothing = value;
                Debug.Log("dkdkdkdkd");
                Debug.Log(flag_smoothing);
            }
        }


        /////////////////////////////////////////////////////////////////

        void Awake()
        {
            m_SessionOrigin = GetComponent<ARSessionOrigin>();
            m_RaycastManager = GetComponent<ARRaycastManager>();
            flag_smoothing = false;
        }

        void Update()
        {
            if (Input.touchCount == 0 || m_Content == null)
                return;

            var touch = Input.GetTouch(0);

            if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = s_Hits[0].pose;

                //////////////////////////// TO DO #2 ///////////////////////////
                if (flag_smoothing)
                {
                    content.transform.position = Vector3.MoveTowards(content.transform.position, hitPose.position, Time.deltaTime * moveSpeed);
                }
                else
                {
                    m_SessionOrigin.MakeContentAppearAt(content, hitPose.position, m_Rotation);
                }
                /////////////////////////////////////////////////////////////////
            }
        }

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARSessionOrigin m_SessionOrigin;

        ARRaycastManager m_RaycastManager;
    }
}