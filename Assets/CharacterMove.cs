using UnityEngine;

public class CharacterMover2D : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Rigidbody rb;
    void Start()
    {
       
    }

    public void SetRigidbody(Rigidbody rigidbody)
    {
        rb = rigidbody;
    }

    // Update is called once per frame
    
}
