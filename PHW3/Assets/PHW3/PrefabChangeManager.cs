using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation.Samples
{
    [RequireComponent(typeof(ARTrackedImageManager))]
    public class PrefabChangeManager : MonoBehaviour
    {
        [SerializeField]
        GameObject[] m_Prefabs;
        PrefabImagePairManager p;
        ARTrackedImageManager imp;


        private void Awake()
        {
            p = FindObjectOfType<PrefabImagePairManager>();
        }

        void OnGUI()
        {
            var fontSize = 50;
            GUI.skin.button.fontSize = fontSize;
            GUI.skin.label.fontSize = fontSize;
            float margin = 100f;
            GUILayout.BeginArea(new Rect(margin, margin, Screen.width - margin * 2, Screen.height - margin * 2));

            /////// TO DO #2 [Augment prefabs for target images] ///////
            int tot = m_Prefabs.Length;
            for (int i =0; i < tot; i++)
            {
                if (GUI.Button(new Rect(50.0f, 100.0f * i, 700.0f, 60.0f), m_Prefabs[i].name))
                {
                    p.SetPrefabForReferenceImage(p.imageLibrary[0], m_Prefabs[i]);
                }
            }
            

            ////////////////////////////////////////////////////////////

            GUILayout.EndArea();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            /////// TO DO #2 [Augment prefabs for target images] ///////
            // Note : Utilize class PrefabImagePairManager.SetPrefabForReferenceImage




            ////////////////////////////////////////////////////////////

        }
    }
}
