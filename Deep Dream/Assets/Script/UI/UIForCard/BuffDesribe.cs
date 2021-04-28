using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffDesribe : MonoBehaviour
{
    private Image render;
    private float alpha;
    [SerializeReference] private float deltAlpha = 0.01f;

    private void Awake()
    {
        render = gameObject.GetComponent<Image>();
    }

    private void OnEnable()
    {
        alpha = 0;
        render.color = new Color(render.color.r,render.color.g,render.color.b,0);
        StartCoroutine(AppearTX());
    }

    IEnumerator AppearTX() 
    {
        while (alpha < 1) 
        {
            alpha += deltAlpha;
            render.color = new Color(render.color.r, render.color.g, render.color.b, alpha);
            yield return new WaitForFixedUpdate();
        }
    }
}
