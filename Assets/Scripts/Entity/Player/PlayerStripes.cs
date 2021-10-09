using UnityEngine;

public class PlayerStripes : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private ConfigurableUpdate _configurableUpdate;

    [Header("Preferences")]
    [SerializeField] private int _updateFramerate  = 10;

    private readonly int IsMoving = Animator.StringToHash("IsMoving");

    private void Awake()
    {
        _configurableUpdate.StartUpdate(_updateFramerate, () =>
        {
            MoveStripes();
        });
    }

    private void MoveStripes()
    {
        _animator.SetBool(IsMoving, _rigidbody2D.velocity != Vector2.zero);
    }
}
