using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 MenuPos, GamePos, ShopPos;
    [SerializeField] private Vector3 CurrentPos;
    [SerializeField] private Transform Target;
    [SerializeField] private float speed;
    [SerializeField] private float threshold;

    void OnEnable()
    {
        EventManager.OnChangeGameState += ChangeState;
    }

    void OnDisable()
    {
        EventManager.OnChangeGameState -= ChangeState;
    }

    void Update()
    {
        // if(transform.position != Target.position+CurrentPos)
        // {
        //     transform.position = Vector3.Lerp(transform.position, Target.position+CurrentPos, speed); 
        // }

        Vector3 targetPosition = Target.position + CurrentPos;
    
    
        transform.position = Vector3.Lerp(
        transform.position, 
        targetPosition, 
        speed * Time.deltaTime
        );
    
   
        if(Vector3.Distance(transform.position, targetPosition) < threshold)
        {
            transform.position = targetPosition; 
        }
    }

    void ChangeState(EventManager.GameState gameState)
    {
        Vector3 angles = transform.eulerAngles;
        switch (gameState)
        {
            case EventManager.GameState.Menu:
                CurrentPos = MenuPos;
                angles.x = 20f; 
                transform.eulerAngles = angles;
                break;
            case EventManager.GameState.Shop:
                CurrentPos = ShopPos;
                angles.x = 20f; 
                transform.eulerAngles = angles;
                break;
            case EventManager.GameState.Game:
                CurrentPos = GamePos;
                angles.x = 50f; 
                transform.eulerAngles = angles;
                break;
        }
    }
}
