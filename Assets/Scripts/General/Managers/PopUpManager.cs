using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager instance;

    [SerializeField] private GameObject[] popUpPrefabs;
    [SerializeField] private GameObject canvas;
    [SerializeField] private float spawnInterval = 15f;
    [SerializeField] private int numPopUpsToShow = 2;
    [SerializeField] private int maxPopUpCount = 4;
    [SerializeField] private List<GameObject> activePopUps = new List<GameObject>();

    [SerializeField] public bool isDebuffTriggered = false;

    private float spawnCooldown = 0f;

    private const string POPUP_DEBUFF_KEY = "PopUpDebuff";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadPopUpDebuff();
    }

    void Update()
    {
        if (isDebuffTriggered && spawnCooldown <= 0f)
        {
            StartCoroutine(SpawnPopUps());
            spawnCooldown = spawnInterval;
        }

        spawnCooldown -= Time.deltaTime;
    }

    public void OffThisManager()
    {
        isDebuffTriggered = false;
        SavePopUpDebuff();
    }

    public void OnThisManager()
    {
        isDebuffTriggered = true;
        SavePopUpDebuff();
    }

    IEnumerator SpawnPopUps()
    {
        while (true)
        {
            if (activePopUps.Count < maxPopUpCount)
            {
                for (int i = 0; i < numPopUpsToShow; i++)
                {
                    GameObject popUp = Instantiate(popUpPrefabs[Random.Range(0, popUpPrefabs.Length)]);
                    popUp.transform.SetParent(canvas.transform, false);

                    float randomX = Random.Range(-200f, 200f);
                    float randomY = Random.Range(-100f, 100f);
                    popUp.transform.localPosition = new Vector3(randomX, randomY, 0f);

                    activePopUps.Add(popUp);
                }
            }
            activePopUps.RemoveAll(popUp => popUp == null);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SavePopUpDebuff()
    {
        PlayerPrefs.SetInt(POPUP_DEBUFF_KEY, isDebuffTriggered ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadPopUpDebuff()
    {
        int debuffStatus = PlayerPrefs.GetInt(POPUP_DEBUFF_KEY, 0);
        isDebuffTriggered = debuffStatus == 1;
    }
}