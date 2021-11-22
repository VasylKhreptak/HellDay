using UnityEngine;

public class Human : DamageableObject, IDamageable
{
    public void SaveHuman()
    {
        Debug.Log("Human Saved!");
    }
}
