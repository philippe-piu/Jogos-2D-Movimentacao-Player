using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Script")]
    private PlayerMovement playerMovement;

    public void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.Movement();
        playerMovement.Run();
        playerMovement.Jump();
        playerMovement.Crouch();
    }


}
