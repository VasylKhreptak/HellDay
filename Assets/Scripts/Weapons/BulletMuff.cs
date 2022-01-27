using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletMuff : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [Header("Bullet Muff Data")]
    [SerializeField] private BulletMuffData _data;
    
    private Tween _waitTween;
    
    public void OnEnable()
    {
        SetMovement();

        if (gameObject.activeSelf)
        {
            _waitTween = this.DOWait(_data.LifeTime).OnComplete(() => { gameObject.SetActive(false); });
        }
    }

    private void OnDisable()
    {
        _waitTween.Kill();
    }

    private void SetMovement()
    {
        _rigidbody2D.velocity =
            new Vector2(
                Random.Range(-_data.MAXHorVelocity, -_data.MINHorVelocity) *
                PlayerFaceDirectionController.FaceDirection, _data.VertVelocity);

        _rigidbody2D.AddTorque(Random.Range(-_data.Torque, _data.Torque));
    }
}