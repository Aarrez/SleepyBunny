using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Player : MonoBehaviour
{
    private CharacterController _character;

    private CancellationToken _stopMoveing;

    private void Awake()
    {
        _stopMoveing = new CancellationToken();
        _character = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        InputScript.Moveing += DoMove;
    }

    private void DoMove()
    {
        UpdateMove();
    }

    private async UniTaskVoid UpdateMove()
    {
        await UniTask.WaitForFixedUpdate();
        
    }
    
}
