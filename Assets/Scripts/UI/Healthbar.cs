using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Image healthImage;
	private void Awake()
	{
        healthImage = GetComponent<Image>();
        healthImage.type = Image.Type.Filled;
        healthImage.fillMethod = Image.FillMethod.Horizontal;
        healthImage.fillOrigin = (int)Image.OriginHorizontal.Left;
	}
	// Start is called before the first frame update
	void Start()
    {
        //healthImage.fillAmount = 0.25f;
    }

    public void UpdateHealthFill(float currentAmount, float maxAmount)
	{
        healthImage.fillAmount = currentAmount / maxAmount;
	}

}
