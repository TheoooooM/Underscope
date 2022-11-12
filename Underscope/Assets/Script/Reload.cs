using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    public RectTransform slider;
    public GameObject reloadBar;
    public AnimationCurve curve;
    public Vector2 perfectRange = new Vector2(100, 115);
    public Vector2 activeRange = new Vector2(116, 155);

    public float standardReload = 3.0f;
    public float activeReload = 2.25f;
    public float perfectReload = 1.8f;
    public float failedReload = 4.1f;

    public State state = State.READY;
    public enum State { READY, FIRING, RELOADING,};
    
    private Coroutine _reload;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.BeginReload();
        }

        if (Input.GetKeyDown(KeyCode.Space) && state == State.RELOADING)
        {
            ManualReload();
        }
    }

    public void BeginReload()
    {
        Debug.Log("Je commence le reload");
        reloadBar.SetActive(true);
        _reload = StartCoroutine(Reloading());
    }

    public void ManualReload()
    {
        float value = slider.anchoredPosition.x;
        if (value >= perfectRange.x && value <= perfectRange.y) PerfectReload(value);
        else if (value >= activeRange.x && value <= activeRange.y) ActiveReload(value);
        else FailedReload(value);
        
        if (_reload != null) StopCoroutine(_reload);
    }

    private IEnumerator Reloading()
    {
        yield return new WaitForEndOfFrame();
        state = State.RELOADING;
        for (float i = 0; i < 1; i+= Time.deltaTime / standardReload)
        {
            float value = Mathf.Lerp(0, 300, curve.Evaluate(i));
            slider.anchoredPosition = new Vector2(value,0);
            yield return null;
        }

        StartCoroutine(FinishReload(0.0f, false));
    }

    private IEnumerator FinishReload(float duration, bool perfect)
    {
        yield return new WaitForSeconds(duration);
        state = State.RELOADING;
        slider.anchoredPosition = new Vector2(0, 0);
        if (perfect)
        {
           
        }
        else
        {
            
        }
        reloadBar.SetActive(false);
    }

    private void PerfectReload(float value)
    {
        float t = Mathf.InverseLerp(0, 300, value);
        float remaining = perfectReload - (t * standardReload);
        StartCoroutine(FinishReload(remaining, false));
        Debug.Log("PERFECT");
    }
    
    private void ActiveReload(float value)
    {
        float t = Mathf.InverseLerp(0, 300, value);
        float remaining = activeReload - (t * standardReload);
        StartCoroutine(FinishReload(remaining, false));
        Debug.Log("RELOAD");
    }
    
    private void FailedReload(float value)
    {
        float t = Mathf.InverseLerp(0, 300, value);
        float remaining = failedReload - (t * standardReload);
        StartCoroutine(FinishReload(remaining, false));
        Debug.Log("FAILED");
    }
    
    
    
    
}
