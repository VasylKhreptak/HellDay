using UnityEngine;

public class Human : Entity, IKillable
{
    public void SaveHuman()
    {
        Debug.Log("Human Saved!");
    }
}
