using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimationHandler : MonoBehaviour
{
    [SerializeField] private GameObject _activeObject;

    [SerializeField] Image _fadeImage;
    public void FadeOutAndIn(GameObject newObject)
	{
        StartCoroutine(FadeInNewObject(newObject));
	}

    //This serves to fix the problem where if an object is selected when it is not enabled, it won't highlight
    public void SelectAfterSecond(Selectable selectable)
    {
        StartCoroutine(WaitAndSelect(selectable));
    }

    private IEnumerator WaitAndSelect(Selectable selectable)
	{
        yield return new WaitForSeconds(1f);
        selectable.Select();
	}

    IEnumerator FadeInNewObject(GameObject newObject)
	{
        float f = 0;
        float duration = 0.5f;
        while (f < duration)
		{
            _fadeImage.color = Color.Lerp(Color.clear, Color.black, f/duration);
            f += Time.deltaTime;
            yield return null;
        }
        if (_activeObject != null)
            _activeObject.SetActive(false);

        
        newObject.SetActive(true);
        _activeObject = newObject;

        f = 0;
        while (f < duration)
        {
            _fadeImage.color = Color.Lerp(Color.black, Color.clear, f / duration);
            f += Time.deltaTime;
            yield return null;
        }
    }
}
