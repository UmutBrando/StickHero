using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManager : MonoBehaviour
{
    [SerializeField] private StickController sticksPrefab;
    [SerializeField] private PillarManager pillarManager;
    
    [SerializeField] private Transform targetRotate;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private float offSetX;
    [SerializeField] private ColliderDetect colliderDetect;

    private StickController _current;

    void Start()
    {
    
        
        Create();
    
    
    }

    
    public void Create()
    {


        var position = pillarManager.currentPillarPosition;
        position.x += offSetX;

        var stick = Instantiate(sticksPrefab, position, Quaternion.identity);
        _current = stick;          
            
    }


  public void OnPointerDown()
    {
        _current.grow = true;
    }
    

  public void OnPointerUp()
    {
        _current.grow = false;
        
        IEnumerator Do()
        {
            var rotate = animationController.Rotate(_current.transform, targetRotate);
            yield return rotate;
            yield return null;
            colliderDetect.LevelController(_current.colliderPosition.position);
            yield return new WaitForSeconds(0.1f);
            if (colliderDetect.LevelPass)
                pillarManager.NextLevel();
            else
                Debug.Log("Game Over");
        }

        StartCoroutine(Do());
    }

}
