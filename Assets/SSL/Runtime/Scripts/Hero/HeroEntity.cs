using UnityEngine;

public class HeroEntity : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Horizontal Movements")]
    [SerializeField]private HeroHorizontalMovementsSettings _movementsSettings;
    private float _horizontalSpeed = 0f;
    private float _moveDirX = 0f;

    [Header("Vertical Movements")]
    private float _verticalSpeed = 0f;

    /*code pour le dash (ne fonctionne pas)
    [Header("Dash")]
    [SerializeField] private HeroDashSettings _dashSettings;*/

    [Header("Orientation")]
    [SerializeField] private Transform _orientVisualRoot;
    private float _orientX = 1f;

    [Header("Fall")]
    [SerializeField] private HeroFallSettings _fallSettings;

    [Header("Debug")]
    [SerializeField] private bool _guiDebug = false;

    public void SetMoveDirX(float dirX)
    {
        _moveDirX = dirX;
    }

    private void FixedUpdate()
    {
        /*code pour le dash (ne fonctionne pas)
        if (DashManager.IsDashing)
        {
            return;
        }*/

        if (_AreOrientAndMovementOpposite()) {
            _TurnBack();
        }
        else {
            _UpdateHorizontalSpeed();
            _ChangeOrientFromHorizontalMovement();
        }

        _ApplyFallGravity();

        _ApplyHorizontalSpeed();
        _ApplyVerticalSpeed();
    }

    private void _ChangeOrientFromHorizontalMovement()
    {
        if (_moveDirX == 0f) return;
        _orientX = Mathf.Sign(_moveDirX);
    }

    private void _ApplyHorizontalSpeed()
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = _horizontalSpeed * _orientX;
        _rigidbody.velocity = velocity;
    }
    
    private void Update()
    {
        _UpdateOrientVisual();
    }

    private void _UpdateOrientVisual()
    {
        Vector3 newScale = _orientVisualRoot.localScale;
        newScale.x = _orientX;
        _orientVisualRoot.localScale = newScale;
    }

    private void OnGUI()
    {
        if (!_guiDebug) return;

        GUILayout.BeginVertical(GUI.skin.box);
        GUILayout.Label(gameObject.name);
        GUILayout.Label($"MoveDirX = {_moveDirX}");
        GUILayout.Label($"OrientX = {_orientX}");
        GUILayout.Label($"Horizontal Speed = {_horizontalSpeed}");
        GUILayout.Label($"Vertical = {_verticalSpeed}");
        GUILayout.EndVertical();
    }

    //ajout d'un code pour l'accéleration du hero
    private void _Accelerate()
    {
        _horizontalSpeed += _movementsSettings.acceleration * Time.fixedDeltaTime;
        if (_horizontalSpeed > _movementsSettings.speedMax ) {
            _horizontalSpeed = _movementsSettings.speedMax;
        }
    }

    //ajout d'un code pour la déceleration du hero
    private void _Decelerate()
    {
        _horizontalSpeed -= _movementsSettings.deceleration * Time.fixedDeltaTime;
        if (_horizontalSpeed < 0f) {
            _horizontalSpeed = 0f;
        }
    }

    private void _UpdateHorizontalSpeed()
    {
        if (_moveDirX != 0f)
        {
            _Accelerate();
        }
        else
        {
            _Decelerate();
        }
    }

    //ajout d'un code pour le turnback du hero
    private void _TurnBack()
    {
        _horizontalSpeed -= _movementsSettings.turnBackFrictions * Time.fixedDeltaTime;
        if (_horizontalSpeed < 0f) {
            _horizontalSpeed = 0f;
            _ChangeOrientFromHorizontalMovement();
        }
    }

    private bool _AreOrientAndMovementOpposite()
    {
        return _moveDirX * _orientX < 0f;
    }

    /*ajout d'un code pour activer le dash du hero et vérifier si le dash est en cours ou non (ne fonctionne pas)
    private bool _isDashing = false;

    public void ActivateDash()
    {
        if (!_isDashing)
        {
            _isDashing = true;
            _horizontalSpeed = _dashSettings.speed * _orientX;
            Invoke(nameof(DeactivateDash), _dashSettings.duration);
        }
    }

    private void DeactivateDash()
    {
        _isDashing = false;
        _horizontalSpeed = 0f;
    }*/
    private void _ApplyFallGravity()
    {
        _verticalSpeed -= _fallSettings.fallGravity * Time.fixedDeltaTime;
        if (_verticalSpeed < -_fallSettings.fallSpeedMax) 
            _verticalSpeed = -_fallSettings.fallSpeedMax;
    }
    
    private void _ApplyVerticalSpeed()
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.y = _verticalSpeed;
        _rigidbody.velocity = velocity;
    }
}