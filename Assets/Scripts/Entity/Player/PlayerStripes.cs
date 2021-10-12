 using UnityEngine;

public class PlayerStripes : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;

    [Header("Preferences")]
    [SerializeField] private int _updateFramerate  = 10;

    private readonly int IsMoving = Animator.StringToHash("IsMoving");
    
    private Coroutine _configurableUpdate;

    private void OnEnable()
    {
        ConfigurableUpdate.StartUpdate(this, ref _configurableUpdate,
                _updateFramerate, () => { MoveStripes(); });
    }

    private void OnDisable()
    {
        ConfigurableUpdate.StopUpdate(this, ref _configurableUpdate);
    }

    private void MoveStripes()
    {
        _animator.SetBool(IsMoving, _rigidbody2D.velocity != Vector2.zero);
    }
}
