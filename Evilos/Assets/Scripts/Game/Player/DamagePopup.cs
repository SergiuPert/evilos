using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float dissapearTimer;
    private Color textColor;
    private Vector3 moveVector;
    private float dissapearTimerMax = 0.4f;
    private static int sortingOrder;

    public static DamagePopup Create(Vector3 position, int damageAmount, bool isCriticalHit)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.Instance.damagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit);
        return damagePopup;
    }

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, bool isCriticalHit)
    {
        textMesh.SetText(damageAmount.ToString());
        if (isCriticalHit)
        {
            textMesh.fontSize = 25;
            textColor = new Color(1f, 0f, 0f);
        }
        else if (!isCriticalHit)
        {
            textMesh.fontSize = 15;
            textColor = new Color(1f, 0.7764706f, 0.02352941f);
        }
        textMesh.color = textColor;
        dissapearTimer = dissapearTimerMax;
        moveVector = new Vector3(1f, 1f) * 30f;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }
    // Update is called once per frame
    void Update()
    {
        if (dissapearTimer > 0)
        {
            transform.position += moveVector * Time.deltaTime;
            moveVector -= moveVector * 8f * Time.deltaTime;
        }

        if (dissapearTimer > dissapearTimerMax/2)
        {
            float increaseScaleAmount = 3f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        //else
        //{
        //    float decreaseScaleAmount = 2f;
        //    transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        //}

        dissapearTimer -= Time.deltaTime;
        if(dissapearTimer < 0)
        {
            float disappearSpeed = 5f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
