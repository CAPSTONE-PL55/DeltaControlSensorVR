using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeatmapSettings = HeatmapVisualization.HeatmapTextureGenerator.Settings;


namespace HeatmapVisualization
{
	public class Heatmap : MonoBehaviour
	{
		#region Settings
		[SerializeField]
		private ComputeShader gaussianComputeShader;
		[SerializeField]
		public Vector3Int resolution = new Vector3Int(64, 64, 64);
		[SerializeField]
		public HeatmapSettings.CutoffMethod cutoffMethod;
		[SerializeField]
		[Range(0.0f, 1.0f)]
		public float cutoffPercentage = 1.0f;
		[SerializeField]
		public float gaussStandardDeviation = 1.0f;
		[SerializeField]
		private Gradient colormap1;
		[SerializeField]
		private Gradient colormap2;
		[SerializeField]
		private Gradient colormap3;
		[SerializeField]
		private Gradient colormap4;
		[SerializeField]
		private Gradient colormap5;
		[SerializeField]
		private bool renderOnTop = false;
		[SerializeField]
		private FilterMode textureFilterMode = FilterMode.Bilinear;
		[SerializeField]
        public double Temperature = 20.0;
		
        
		private const int colormapTextureResolution = 256;
		#endregion


		#region Globals
		private MeshRenderer ownRenderer;
		private MeshRenderer OwnRenderer { get { if (ownRenderer == null) { ownRenderer = GetComponent<MeshRenderer>(); } return ownRenderer; } }
		public Bounds BoundsFromTransform { get => new Bounds { center = transform.position, size = transform.localScale }; }
		#endregion
        
		#region data
		[SerializeField]
		public GameObject mqttReceiver;
		#endregion

		#region Functions

		void Start()
		{
        	InvokeRepeating("getreadings", 0.0f, 1.0f);
		}

	    void getreadings()
        {
        	Temperature = mqttReceiver.GetComponent<mqttReceiver1>().readings.temperature;
			SetColormap();
        }

		public void GenerateHeatmap(List<Vector3> points)
		{
			if (points == null)
			{
				return;
			}

			HeatmapSettings settings = new HeatmapSettings(BoundsFromTransform, resolution, cutoffMethod, cutoffPercentage, gaussStandardDeviation);
			HeatmapTextureGenerator heatmapTextureGenerator = new HeatmapTextureGenerator(gaussianComputeShader);
			Texture3D heatTexture = heatmapTextureGenerator.CalculateHeatTexture(points, settings);
			heatTexture.wrapMode = TextureWrapMode.Clamp;
			heatTexture.filterMode = textureFilterMode;
			heatTexture.Apply();

			SetHeatTexture(heatTexture);
			SetColormap();
			SetRenderOnTop();
			SetTextureFilterMode();
		}

        
		private void SetHeatTexture(Texture3D heatTexture)
		{
			Material material = new Material(OwnRenderer.sharedMaterial); //not edit the material asset
			material.SetTexture("_DataTex", heatTexture);
			OwnRenderer.sharedMaterial = material;
		}

		public void SetColormap(Gradient colormap1)
		{
			this.colormap1 = colormap1;
			SetColormap();
		}


		public void SetColormap()
		{
			if(Temperature < 10)
			{
				OwnRenderer.sharedMaterial.SetTexture("_GradientTex", GradientToTexture(colormap1, colormapTextureResolution));
			}
			else if(Temperature < 15)
			{
				OwnRenderer.sharedMaterial.SetTexture("_GradientTex", GradientToTexture(colormap2, colormapTextureResolution));
			}
			else if(Temperature < 20)
			{
				OwnRenderer.sharedMaterial.SetTexture("_GradientTex", GradientToTexture(colormap3, colormapTextureResolution));
			}
			else if(Temperature < 25)
			{
				OwnRenderer.sharedMaterial.SetTexture("_GradientTex", GradientToTexture(colormap4, colormapTextureResolution));
			}
			else
			{
				OwnRenderer.sharedMaterial.SetTexture("_GradientTex", GradientToTexture(colormap5, colormapTextureResolution));
			}

		}


		public void SetRenderOnTop(bool renderOnTop)
		{
			this.renderOnTop = renderOnTop;
			SetRenderOnTop();
		}


		public void SetRenderOnTop()
		{
			if (renderOnTop)
			{
				OwnRenderer.sharedMaterial.DisableKeyword("USE_SCENE_DEPTH");
			}
			else
			{
				OwnRenderer.sharedMaterial.EnableKeyword("USE_SCENE_DEPTH");
			}
		}


		public void SetTextureFilterMode(FilterMode textureFilterMode)
		{
			this.textureFilterMode = textureFilterMode;
			SetTextureFilterMode();
		}


		public void SetTextureFilterMode()
		{
			OwnRenderer.sharedMaterial.GetTexture("_DataTex").filterMode = textureFilterMode;
		}


		private Texture2D GradientToTexture(Gradient gradient, int resolution)
		{
			Texture2D texture = new Texture2D(resolution, 1);

			for (int i = 0; i < resolution; i++)
			{
				texture.SetPixel(i, 1, gradient.Evaluate(((float)i) / (resolution - 1)));
			}

			texture.wrapMode = TextureWrapMode.Clamp;
			texture.filterMode = FilterMode.Bilinear;
			texture.Apply();
			return texture;
		}
		#endregion
	}
}