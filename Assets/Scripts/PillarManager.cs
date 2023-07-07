using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarManager : MonoBehaviour
{
    [SerializeField] private PillarController pillarPrefab;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private StickManager stickManager;

    [SerializeField] private Transform targetMin;
    [SerializeField] private Transform targetMax;

    [SerializeField] private Transform camera;

    [SerializeField] private GameObject player;


    public Vector3 currentPillarPosition => current.transform.position;
    [SerializeField] private PillarController current;
    private PillarController _targetPillar;

    private Vector3 _offSetCamera;

    private void Start()
    {
        _offSetCamera = camera.transform.position - current.transform.position;


        Create();
    }

    [ContextMenu(nameof(Create))]
    public void Create()
    {
        var pillar = Instantiate(pillarPrefab);


        //Set Position
        ChangePositionx(pillar.transform,spawnPoint.position.x);


        //Set Scale
        pillar.SetRandomSize();


        //Set Target
        var targetX = Random.Range(targetMin.position.x, targetMax.position.x);
        var targetPosition = pillar.transform.position;
        targetPosition.x = targetX;
        var move = animationController.Move(pillar.transform, targetPosition);


        //Animation
        StartCoroutine(move);


        //Set Target
        _targetPillar = pillar;
    }


    private void ChangePositionx(Transform current, float x)
    {
        var position = current.transform.position;
        position.x = x;
        current.transform.position = position;
    }
    public void NextLevel()
    {
        current = _targetPillar;
 
        //Camera Animation
        var targetPosition = current.transform.position + _offSetCamera;
        var move = animationController.Move(camera, targetPosition, 0.2f);
        StartCoroutine(move);
       IEnumerator Do()
        {
            yield return new WaitForSeconds(0.3f);
            Create();

            stickManager.Create();
        
        }
        
        StartCoroutine(Do());

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            NextLevel();
        }
    }

}
