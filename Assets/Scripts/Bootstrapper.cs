using Character.Implementations;
using Character.Interfaces;
using General.Implementations;
using General.Interfaces;
using InputController.Interfaces;
using UnityEngine;
using World.MapBuildController.Controllers;
using World.MapBuildController.Views;
using World.MapGenerator.Implementations;
using World.MapGraphicsBuilder.Implementations;
using World.MapModel.Data;
using World.MapModel.Interfaces;
using World.MapPathFinder.Implementations;

[RequireComponent(typeof(IInputController))]
public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private MapBuildControllerView _buildControllerView;

    private IUnityEventsMediator _unityEventsMediator;
    private IMap _map;

    public void Awake()
    {
        var mapGenerator = new MapGenerator();
        var mapGraphicsBuilder = new MapGraphicsBuilder(_cubePrefab);
        var inputController = gameObject.GetComponent(typeof(IInputController)) as IInputController;
        var mapBuildController = new MapBuildController(_buildControllerView, mapGenerator, mapGraphicsBuilder, inputController);

        var characterFactory = new CharacterFactory();
        var characterSpawner = new CharacterSpawner();
        var character = characterFactory.CreateCharacter(_playerPrefab);

        var mapPathFinder = new AStarPathFinder();
        var mapPathDrawer = new MapPathDrawer();

        inputController.OnClickOnObject += obj =>
        {
            var path = mapPathFinder.FindPath(
                map: _map,
                start: new Point((int)character.Position.x, (int)character.Position.z),
                end: new Point((int)obj.transform.position.x, (int)obj.transform.position.z)
            );

            if (path != null)
            {
                mapPathDrawer.DrawPath(path);
                character.SetMovePath(path, mapPathDrawer.Clear);
            }
        };

        mapBuildController.OnMapBuilded += map =>
        {
            _map = map;
            mapPathDrawer.Clear();
            characterSpawner.SpawnToRandomPoint(map, character);
        };

        mapBuildController.BuildControlls();
        mapBuildController.BuildMap();

        _unityEventsMediator = new UnityEventsMediator();
        _unityEventsMediator.Register(inputController);
        _unityEventsMediator.Register(character);
    }

    private void Update()
    {
        _unityEventsMediator?.OnUpdate(Time.deltaTime);
    }
}
