using TMPro;
using UnityEngine;

public class EvaluationManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI rankDescriptionText;

    private void Start()
    {
        DisplayRank();
    }

    public void DisplayRank()
    {
        int completed = GameModeManager.instance.GetCompletedMissionCount();
        string rank = GetRankFromCompletedMissions(completed);
        string colorHex = GetRankColorHex(rank);
        string description = GetRankDescription(rank);

        rankText.text = $"<color={colorHex}><b>{rank}</b></color>";
        rankDescriptionText.text = description;

        rankText.outlineColor = Color.black;
        rankText.outlineWidth = 0.15f;
    }

    private string GetRankFromCompletedMissions(int count)
    {
        return count switch
        {
            6 => "Sentinel",
            5 => "Anomaly",
            4 => "Breacher",
            3 => "Cryptor",
            2 => "Disruptor",
            _ => "Faker"
        };
    }

    private string GetRankColorHex(string rank)
    {
        return rank switch
        {
            "Sentinel" => "#FFD700", // Gold
            "Anomaly" => "#FFA500",  // Orange
            "Breacher" => "#FF0000", // Red
            "Cryptor" => "#0000FF",  // Blue
            "Disruptor" => "#800080", // Purple
            "Faker" => "#808080",    // Grey
            _ => "#FFFFFF"
        };
    }

    private string GetRankDescription(string rank)
    {
        return rank switch
        {
            "Sentinel" => "The system’s unseen guardian. Operates with unmatched precision, foresight, and control. A legend in the net.",
            "Anomaly" => "A rare variable in the digital realm. Powerful, adaptive, and almost undetectable. One step away from mastery.",
            "Breacher" => "A skilled infiltrator who broke through strong defenses. Effective, persistent, and highly capable.",
            "Cryptor" => "Standard performance. Played it safe, kept things contained. There's potential waiting to be unlocked.",
            "Disruptor" => "Showed signs of promise but faltered. Chaos without clarity — refinement is needed.",
            "Faker" => "A shadow with no substance. Failed to leave any meaningful trace in the system.",
            _ => ""
        };
    }
}
