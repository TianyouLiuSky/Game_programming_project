using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public abstract class PostEffectsBase : MonoBehaviour
    {
        protected bool supportHDR = true;
        protected bool supportDX11 = false;
        protected bool isSupported = true;

        protected Material CheckShaderAndCreateMaterial(Shader s, Material m2Create)
        {
            if (!s)
            {
                Debug.Log("Missing shader in " + ToString());
                enabled = false;
                return null;
            }

            if (s.isSupported && m2Create && m2Create.shader == s)
                return m2Create;

            if (!s.isSupported)
            {
                NotSupported();
                Debug.LogError("The shader " + s.ToString() + " on effect " + ToString() + " is not supported on this platform!");
                return null;
            }

            m2Create = new Material(s);
            m2Create.hideFlags = HideFlags.DontSave;
            
            return m2Create;
        }

        protected bool CheckSupport()
        {
            return CheckSupport(false);
        }

        protected bool CheckSupport(bool needDepth)
        {
            isSupported = true;
            supportHDR = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf);
            supportDX11 = SystemInfo.graphicsShaderLevel >= 50 && SystemInfo.supportsComputeShaders;

            if (!SystemInfo.supportsImageEffects)
            {
                NotSupported();
                return false;
            }

            if (needDepth && !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
            {
                NotSupported();
                return false;
            }

            if (needDepth)
                GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;

            return true;
        }

        protected void NotSupported()
        {
            enabled = false;
            isSupported = false;
        }

        protected void ReportAutoDisable()
        {
            Debug.LogWarning("The image effect " + ToString() + " has been disabled as it's not supported on the current platform.");
        }

        // Abstract method that derived classes should implement
        public abstract bool CheckResources();

        protected void Start()
        {
            CheckResources();
        }

        protected bool CheckShader(Shader s)
        {
            Debug.Log("The shader " + s.ToString() + " on effect " + ToString() + " is not part of the Unity Standard Assets. For best performance and quality, please ensure you are using the latest Standard Assets Image Effects from the Asset Store.");
            if (!s.isSupported)
            {
                NotSupported();
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}