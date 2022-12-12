using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class OtherGrab : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public bool Grabed = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        gameObject.layer = LayerMask.NameToLayer("Grabable");
        if (gameObject.CompareTag("Untagged"))
        {
            gameObject.tag = "Move_Object";
        }
    }

    public void ObjectGrabed(bool grabed)
    {
        Grabed = grabed;
    }
}