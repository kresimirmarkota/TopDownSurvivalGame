using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
     private Transform mapTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position=mapTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playerMovement()
    {
        if (Input.GetMouseButton(0))
        {

        }
    }
}
