using UnityEngine;

public class FoodObject : CellObject
{
    public override void PlayerEntered()
    {
        Destroy(gameObject);
        Debug.Log("Food Destroyed");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
