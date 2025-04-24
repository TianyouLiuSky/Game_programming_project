using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FloatingTextHelper : MonoBehaviour
{
    private float speed = 1;
    private float duration = 0.5f;
    private TextMeshPro textMesh;
    private Vector3 startPos;
    private Color startColor;
    private Camera cameraToFace;
    
    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        startPos = transform.position;
        startColor = textMesh.color;
        StartCoroutine(FloatAndFade());
        cameraToFace = FindObjectOfType<Camera>();
    }

    void Update()
    {
        // Make text always face the camera
        if (cameraToFace != null)
        {
            transform.rotation = cameraToFace.transform.rotation;
        }
    }
    
    IEnumerator FloatAndFade()
    {
        float elapsed = 0;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;
            
            // Move upward
            transform.position = startPos + new Vector3(0, progress * speed, 0);
            
            // Fade out
            textMesh.color = new Color(startColor.r, startColor.g, startColor.b, 1 - progress);
            
            yield return null;
        }
        
        Destroy(gameObject);
    }
}