using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject[] objsToShow;
    public GameObject[] objsToHide;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (objsToShow != null)
        {
            foreach (GameObject obj in objsToShow)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }

        if (objsToHide != null)
        {
            foreach (GameObject obj in objsToHide)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (objsToShow != null)
        {
            foreach (GameObject obj in objsToShow)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }

        if (objsToHide != null)
        {
            foreach (GameObject obj in objsToHide)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}