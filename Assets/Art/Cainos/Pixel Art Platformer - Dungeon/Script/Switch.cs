using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.LucidEditor;
using UnityEditor.SceneManagement;
using UnityEditor;

namespace Cainos.PixelArtPlatformer_Dungeon
{
    public class Switch : MonoBehaviour
    {
        [FoldoutGroup("Reference")] public MonoBehaviour target; // Change the type to MonoBehaviour
        [Space]
        [FoldoutGroup("Reference")] public SpriteRenderer spriteRenderer;
        [FoldoutGroup("Reference")] public Sprite spriteOn;
        [FoldoutGroup("Reference")] public Sprite spriteOff;
        
        [SerializeField]
        public AudioSource audioSrc;

        private Animator Animator
        {
            get
            {
                if (animator == null) animator = GetComponent<Animator>();
                return animator;
            }
        }
        private Animator animator;

        private void Start()
        {
            Animator.SetBool("IsOn", isOn);
            IsOn = isOn;
        }

        [FoldoutGroup("Runtime"), ShowInInspector]
        public bool IsOn
        {
            get { return isOn; }
            set
            {
                isOn = value;

#if UNITY_EDITOR
                if (Application.isPlaying == false)
                {
                    EditorUtility.SetDirty(this);
                    EditorSceneManager.MarkSceneDirty(gameObject.scene);
                }
#endif

                if (target)
                {
                    // Check if the target implements the Door interface
                    Door door = target as Door;
                    if (door != null)
                    {
                        door.IsOpened = isOn;
                        if (isOn)
                        {
                            door.Open();
                        }
                    }
                    else
                    {
                        // Check if the target implements the Elevator interface
                        Elevator elevator = target as Elevator;
                        if (elevator != null)
                        {
                            elevator.IsWaiting = isOn;
                            // Additional logic for Elevator, e.g., triggering movement
                            if (isOn)
                            {
                                elevator.Activate();
                            }
                        }
                    }
                }


                if (Application.isPlaying)
                {
                    Animator.SetBool("IsOn", isOn);
                }
                else
                {
                    if (spriteRenderer) spriteRenderer.sprite = isOn ? spriteOn : spriteOff;
                }
            }
        }
        [SerializeField, HideInInspector]
        private bool isOn;

        [FoldoutGroup("Runtime"), HorizontalGroup("Runtime/Button"), Button("Turn On")]
        public void TurnOn()
        {
            if (!isOn)
            {
                audioSrc.Play();
            }
            IsOn = true;
            
            
        }

        [FoldoutGroup("Runtime"), HorizontalGroup("Runtime/Button"), Button("Turn Off")]
        public void TurnOff()
        {
            IsOn = false;
        }
    }
}
