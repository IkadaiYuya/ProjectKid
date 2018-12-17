using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemyUI : MonoBehaviour
{
    [SerializeField] private GameObject lookat;

    private RectTransform rTransform;
    private Vector3 viewPortPos;
    private Vector3 worldPos2d;
    [SerializeField] private Vector3 offset = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        rTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, lookat.transform.position + offset);
    }
}
