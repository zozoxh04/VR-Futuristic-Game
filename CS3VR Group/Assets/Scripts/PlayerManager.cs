using UnityEngine;
using UnityEngine.XR;

public class PlayerSetupManager : MonoBehaviour
{
    [SerializeField] private GameObject pcPlayer;  // Reference to the PC player GameObject
    [SerializeField] private GameObject xrPlayer;  // Reference to the XR player GameObject

    private void Awake()
    {
        // Check if an XR device is present and running
        if (XRSettings.isDeviceActive)
        {
            Debug.Log("XR device detected. Enabling XR Player.");
            ActivatePlayer(xrPlayer);
        }
        else
        {
            Debug.Log("No XR device detected. Enabling PC Player.");
            ActivatePlayer(pcPlayer);
        }
    }

    private void ActivatePlayer(GameObject player)
    {
        // Enable the chosen player and disable the other
        if (pcPlayer != null) pcPlayer.SetActive(player == pcPlayer);
        if (xrPlayer != null) xrPlayer.SetActive(player == xrPlayer);
    }
}