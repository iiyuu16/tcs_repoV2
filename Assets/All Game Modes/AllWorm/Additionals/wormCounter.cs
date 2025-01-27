using TMPro;
using UnityEngine;

public class wormCounter : MonoBehaviour
{
    public GameObject wormObj1;
    public GameObject wormObj2;
    public GameObject wormObj3;
    public TextMeshProUGUI countText;

    private int wormCount = 0;

    private void Start()
    {
        wormCount = CountEnabledObjects();
        UpdateCountText();
    }

    private int CountEnabledObjects()
    {
        int count = 0;
        if (wormObj1.activeSelf) count++;
        if (wormObj2.activeSelf) count++;
        if (wormObj3.activeSelf) count++;
        return count;
    }

    private void Update()
    {
        UpdateCountText();
    }

    private void UpdateCountText()
    {
        wormCount = CountEnabledObjects();
        countText.text = "Worm Level: \n LvL: " + wormCount;
    }
}
