using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView PV;
    private CharacterController characterController;
    public float movementSpeed;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            BasicMovement();
            BasicRotation();
        }
    }
    void BasicMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            characterController.Move(transform.forward * Time.deltaTime * movementSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            characterController.Move(-transform.forward * Time.deltaTime * movementSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            characterController.Move(-transform.right * Time.deltaTime * movementSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            characterController.Move(transform.right * Time.deltaTime * movementSpeed);
        }

    }
    void BasicRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        transform.Rotate(new Vector3(0, mouseX, 0));
    }
}
