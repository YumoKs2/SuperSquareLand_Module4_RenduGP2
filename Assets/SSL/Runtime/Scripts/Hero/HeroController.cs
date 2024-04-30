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

        /*code pour le dash (ne fonctionne pas)
        if (GetInputDash() > 0f)
        {
            DashManager.IsDashing = true;
        }*/
    }

    private float GetInputMoveX()
    {
        float inputMoveX = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q))
        {
            inputMoveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveX = 1f;
        }
        return inputMoveX;
    }

    /*code pour le dash (ne fonctionne pas)
    private float GetInputDash()
    {
        float inputDash = 0f;
        if (Input.GetKeyDown(KeyCode.E))
        {
            inputDash = 1f;
        }
        return inputDash;
    }*/
}