using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovingText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform target;
    public Transform endPoint;

    public Text textRisiko;
    public Text sct;

    Vector3 startPos;
    Vector3 targetPos;

    public float speed = 60;
    int mouseOnCount = 0;
    int clone = 0;
    int savepoint = 0;
    float width;
    float lebarkotak;

    void Update()
    {
        targetPos = target.transform.position;
        startPos.y = targetPos.y;
        width = textRisiko.preferredWidth;
        if (mouseOnCount > 0)
        {
            teksJalan();
        }
    }

    public void teksJalan()
    {
        textRisiko.transform.position += Vector3.left * (speed * Time.deltaTime);
        Vector3 kiriKanan = endPoint.position - target.position;

        if (target.gameObject.activeSelf == false)
        {
            if (kiriKanan.x < -64)
            {
                textRisiko.transform.position = startPos;
            }
        }
        else
        {
            if (kiriKanan.x < -40)
            {
                textRisiko.transform.position = startPos;
            }
        }


    }

    public void createDuplicateText()
    {
        if (clone == 0)
        {
            clone = 1;
            sct = (Text)Instantiate(textRisiko);
            RectTransform TextTransform = textRisiko.GetComponent<RectTransform>();
            RectTransform CloneTransform = sct.GetComponent<RectTransform>();
            CloneTransform.SetParent(TextTransform);
            CloneTransform.localPosition = new Vector3(width + 80, 0, 0);
            CloneTransform.localScale = new Vector3(1, 1, 1);
        }

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (textRisiko.text.Length > 43)
        {
            mouseOnCount = 1;
            if (savepoint == 0)
            {
                startPos.y = targetPos.y;
                startPos = textRisiko.transform.position;
                savepoint++;
            }
            if (clone == 0)
            {
                textRisiko.transform.position = startPos;
                createDuplicateText();
            }
            teksJalan();
        }

    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (textRisiko.text.Length > 43)
        {
            mouseOnCount = 0;
            textRisiko.transform.position = startPos;
            savepoint = 0;
        }
        if (clone == 1)
        {
            clone = 0;
            Destroy(sct.gameObject);
        }
    }
}