using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class MyImageEffect : MonoBehaviour
{

	[SerializeField] Material material;

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(src, dest, material);
	}
}