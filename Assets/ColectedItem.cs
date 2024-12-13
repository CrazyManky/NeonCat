using Project.Screpts.Character;
using UnityEngine;

public class ColectedItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<CharacterMovement>(out CharacterMovement characterMovement))
            Destroy(gameObject);
    }
}