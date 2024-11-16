using UnityEngine;

public class InteractableCube : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Cube interacted with!");
        GetComponent<Renderer>().material.color = Color.red;

    }
}