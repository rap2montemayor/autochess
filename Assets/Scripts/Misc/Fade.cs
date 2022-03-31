using System.Collections;
using UnityEngine;

public class Fade : MonoBehaviour
{


	public void SetVisibility(bool state){SetVisibility(transform, state);}

	public void SetVisibility(Transform tra, bool state){
        Renderer renderer = tra.gameObject.GetComponent<Renderer>();
        if (renderer != null){renderer.enabled = state;}
        foreach(Transform t in tra){SetVisibility(t, state);}
    }

	public void FadeIn(float seconds){FadeIn(seconds, transform);}

	public void FadeIn(float seconds, Transform tra){
		Renderer renderer = tra.gameObject.GetComponent<Renderer>();
		if (renderer != null && (renderer.material.color.a == 0 || renderer.material.color.a == 1)){
			SetVisibility(tra, false);
			StartCoroutine (FadeInCR(seconds, renderer));
		}
		foreach (Transform t in tra){FadeIn(seconds, t);}
	}

	private IEnumerator FadeInCR(float seconds, Renderer r){
		Color new_color;
		float t = 0f;
		while (t <= seconds){
			t += Time.deltaTime;
			new_color = r.material.color; new_color.a = Mathf.Lerp(0,1,t/seconds); r.material.color = new_color;
			yield return null;
		}
		new_color = r.material.color; new_color.a = 1; r.material.color = new_color;
	}


	public void FadeOut(float seconds){FadeOut(seconds, transform);}

	public void FadeOut(float seconds, Transform tra){
		Renderer renderer = tra.gameObject.GetComponent<Renderer>();
		if (renderer != null && (renderer.material.color.a == 0 || renderer.material.color.a == 1)){
			SetVisibility(tra, true);
			StartCoroutine (FadeOutCR(seconds, renderer));
		}
		foreach (Transform t in tra){FadeOut(seconds, t);}
	}

	private IEnumerator FadeOutCR(float seconds, Renderer r){
		Color new_color;
		float t = 0f;
		while (t <= seconds){
			t += Time.deltaTime;
			new_color = r.material.color; new_color.a = Mathf.Lerp(0,1,1 - t/seconds); r.material.color = new_color;
			yield return null;
		}
		new_color = r.material.color; new_color.a = 0; r.material.color = new_color;
	}


}