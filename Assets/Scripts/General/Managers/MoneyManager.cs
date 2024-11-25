using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    public int currentMoney;
    public TextMeshProUGUI moneyText;

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
    }

    private void Start()
    {
        LoadMoney();
        UpdateMoneyText();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetupSceneReferences();
    }

    public void SetupSceneReferences()
    {
        moneyText = GameObject.Find("MoneyText")?.GetComponent<TextMeshProUGUI>();
        UpdateMoneyText();
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        SaveMoney();
        UpdateMoneyText();
        Debug.Log("Added money: " + amount + ". Current money: " + currentMoney);
    }

    public void SubtractMoney(int amount)
    {
        currentMoney -= Mathf.Abs(amount);
        SaveMoney();
        UpdateMoneyText();
        Debug.Log("Subtracted money: " + amount + ". Current money: " + currentMoney);
    }

    public bool SpendMoney(int amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            SaveMoney();
            UpdateMoneyText();
            Debug.Log("Spent money: " + amount + ". Current money: " + currentMoney);
            return true;
        }
        else
        {
            Debug.Log("Not enough money. Current money: " + currentMoney);
            return false;
        }
    }

    public int GetCurrentMoney()
    {
        return currentMoney;
    }

    public void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = "FRGz: " + currentMoney.ToString();
        }
    }

    public void UpdateMoneyFromGamemode(int score)
    {
        int moneyFromScore = score;
        AddMoney(moneyFromScore);
        Debug.Log("added " +moneyFromScore+ " from gamemode");
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt("CurrentMoney", currentMoney);
        PlayerPrefs.Save();
    }

    private void LoadMoney()
    {
        currentMoney = PlayerPrefs.GetInt("CurrentMoney", 0);
    }

    public void ResetMoney()
    {
        currentMoney = 0;
        SaveMoney();
        UpdateMoneyText();
        Debug.Log("Money has been reset. Current money: " + currentMoney);
    }

    private void OnValidate()
    {
        if (moneyText != null)
        {
            UpdateMoneyText();
        }
    }
}
