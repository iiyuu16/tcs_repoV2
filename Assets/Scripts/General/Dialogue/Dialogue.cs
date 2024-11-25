using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string name;
    [TextArea(5,15)]
    public string[] sentences;
}

[System.Serializable]
public class Message
{
    public int actorID;
    public string message;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}