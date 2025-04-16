// Nian Gao, Tianyou Liu, Alina Pan
using UnityEngine;
using System.Collections.Generic;

/**
This script handles the occlusion of objects between the player and the camera
*/
public class PlayerOcclusionHandler : MonoBehaviour
{
    public Transform player;
    public LayerMask occlusionLayers;
    public float transparencyLevel = 0.1f;
    
    // Dictionary to track original materials and their temporary transparent versions
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();
    private List<Renderer> currentlyTransparent = new List<Renderer>();
    
    void LateUpdate()
    {
        // Reset previously transparent objects
        foreach (Renderer renderer in currentlyTransparent)
        {
            if (renderer != null && originalMaterials.ContainsKey(renderer))
            {
                renderer.materials = originalMaterials[renderer];
            }
        }
        
        originalMaterials.Clear();
        currentlyTransparent.Clear();
        
        // Direction from camera to player
        Vector3 direction = player.position - transform.position;
        float distance = Vector3.Distance(transform.position, player.position);
        
        // Cast a ray to find everything between camera and player
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance, occlusionLayers);
        
        // Make all objects hit by the ray semi-transparent, except the player
        foreach (RaycastHit hit in hits)
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject == player.gameObject)
                continue;
                
            // Make all renderers on this object semi-transparent
            Renderer[] renderers = hitObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                if (renderer != null)
                {
                    originalMaterials[renderer] = renderer.materials;
                    currentlyTransparent.Add(renderer);
                    
                    // Create temporary transparent materials
                    Material[] transparentMaterials = new Material[renderer.materials.Length];
                    for (int i = 0; i < renderer.materials.Length; i++)
                    {
                        transparentMaterials[i] = new Material(renderer.materials[i]);
                        transparentMaterials[i].SetFloat("_Mode", 3);
                        transparentMaterials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                        transparentMaterials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        transparentMaterials[i].SetInt("_ZWrite", 0);
                        transparentMaterials[i].DisableKeyword("_ALPHATEST_ON");
                        transparentMaterials[i].EnableKeyword("_ALPHABLEND_ON");
                        transparentMaterials[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
                        transparentMaterials[i].renderQueue = 3000;
                        Color color = transparentMaterials[i].color;
                        color.a = transparencyLevel;
                        transparentMaterials[i].color = color;
                    }

                    renderer.materials = transparentMaterials;
                }
            }
        }
    }
    
    void OnDestroy()
    {
        // Restore original materials
        foreach (Renderer renderer in currentlyTransparent)
        {
            if (renderer != null && originalMaterials.ContainsKey(renderer))
            {
                renderer.materials = originalMaterials[renderer];
            }
        }
    }
}