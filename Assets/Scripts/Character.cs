using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterController character;
    public float moveSpeed;
    public float turnSpeed;
    void Update()
    {
        Vector3 motion = new Vector3(0, 0, Input.GetAxis("Vertical"));
        Vector3 rotation = new Vector3(0,Input.GetAxis("Horizontal"), 0);
        character.Move(motion * moveSpeed * Time.deltaTime);
        character.transform.eulerAngles += rotation * turnSpeed * Time.deltaTime;
    }
}
