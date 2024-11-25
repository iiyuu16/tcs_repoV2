using UnityEngine;
using TMPro;

public class DateTimeDisp : MonoBehaviour
{
    public TMP_Text dateTimeText;

    void Start()
    {
        if (dateTimeText == null)
        {
            dateTimeText = GetComponent<TMP_Text>();
        }
    }

    void Update()
    {
        dateTimeText.text = System.DateTime.Now.ToString("MM/dd  HH:mm");
    }
}

