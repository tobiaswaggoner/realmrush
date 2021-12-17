using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower Tower; 
    [SerializeField] bool isPlacable = false;
    private XRSimpleInteractable _interactable;
    [SerializeField] private MeshRenderer _placementIndicator;
    [SerializeField] private Material _placementPossibleMaterial;
    [SerializeField] private Material _placementImpossibleMaterial;
    
    void Awake()
    {
        _interactable = GetComponent<XRSimpleInteractable>();
        _interactable.hoverEntered.AddListener(OnHoverEntered);
        _interactable.hoverExited.AddListener(OnHoverExited);
        _interactable.activated.AddListener(OnActivated);
        _interactable.selectEntered.AddListener(OnSelectEntered);
        _interactable.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectExited(SelectExitEventArgs arg0)
    {
        if (!isPlacable) return;
        Debug.Log("Exit");
        _placementIndicator.enabled = false;
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (!isPlacable) return;
        Debug.Log("Enter");
        _placementIndicator.enabled = true;
        if (Tower.CanBePurchased)
        {
            _placementIndicator.material = _placementPossibleMaterial;
        }
        else
        {
            _placementIndicator.material = _placementImpossibleMaterial;
        }
        
    }

    private void OnActivated(ActivateEventArgs arg0)
    {
        if(!isPlacable) return;
        
        var placed = Tower.CreateTower(transform.position);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        isPlacable = !placed;
    }

    private void OnHoverExited(HoverExitEventArgs arg0)
    {
        if(!isPlacable) return;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void OnHoverEntered(HoverEnterEventArgs arg0)
    {
        if(!isPlacable) return;
        transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
    }

    public bool IsPlacable => Tower.CanBePurchased && isPlacable;

    private void OnMouseEnter() 
    {
        // if(!isPlacable) return;
        // transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }

    private void OnMouseExit() 
    {
        // if(!isPlacable) return;
        // transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void OnMouseDown() 
    {
        // if(!isPlacable) return;
        //
        // var placed = Tower.CreateTower(transform.position);
        // transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        // isPlacable = !placed;
        
    }

}
