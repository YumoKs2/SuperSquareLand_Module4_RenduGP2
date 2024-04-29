using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class HeroController : MonoBehaviour
{
    [Header("Entity")]
    [SerializeField] private HeroEntity _entity;

    [Header("Debug")]
    [SerializeField] private bool _guiDebug = false;

    private void OnGUI()
    {
        if (!_guiDebug) return;

        GUILayout.BeginVertical(GUI.skin.box);
        GUILayout.Label(gameObject.name);
        GUILayout.EndVertical();
    }

    private void Update()
    {
        _entity.SetMoveDirX(GetInputMoveX());
    }

    private float GetInputMoveX()
    {
        float inputMoveX = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q)) ;
        //Negative means : To the left <=
        inputMoveX = 1f;
    }
    
    if (Input.GetKey(KeyCode.D)) {
        //Positive means : To the right =>
        inputMoveX = 1f;
}
return inputMoveX;
}