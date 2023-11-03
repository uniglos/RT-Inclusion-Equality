using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance { get; private set; }

    [SerializeField] private AudioSource BGMController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(Instance);
        }
    }

    public AudioSource PlaySoundBGM(AudioClip audioSound, BGMNode node)
    {
        BGMController.clip = audioSound;
        BGMController.volume = node.volume;
        BGMController.Play();

        return BGMController;
    }
}
