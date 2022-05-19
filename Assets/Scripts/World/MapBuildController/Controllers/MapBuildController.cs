using InputController.Enums;
using InputController.Interfaces;
using System;
using UnityEngine;
using World.MapBuildController.Interfaces;
using World.MapBuildController.Views;
using World.MapGenerator.Interfaces;
using World.MapGraphicsBuilder.Interfaces;
using World.MapModel.Interfaces;

namespace World.MapBuildController.Controllers
{
    public class MapBuildController : IMapBuildController
    {
        private const int MinSize = 25;
        private const int MaxSize = 50;

        private MapBuildControllerView _view;
        private IMapGenerator _mapGenerator;
        private IMapGraphicsBuilder _mapGraphicsBuilder;
        private IInputController _inputController;

        private int _currentSize = 30;
        private GameObject _mapParent;

        public event Action<IMap> OnMapBuilded;

        public MapBuildController(MapBuildControllerView view, IMapGenerator mapGenerator,
            IMapGraphicsBuilder mapGraphicsBuilder,
            IInputController inputController)
        {
            _view = view;
            _mapGenerator = mapGenerator;
            _mapGraphicsBuilder = mapGraphicsBuilder;
            _inputController = inputController;

            view.DrawSize(_currentSize);
        }

        public void BuildControlls()
        {
            _inputController.AddButtonListener(EInputButtonType.GenerateButton, BuildMap);
            _inputController.AddButtonListener(EInputButtonType.IncreaseMapSizeButton, IncreaseMapSize);
            _inputController.AddButtonListener(EInputButtonType.DecreaseMapSizeButton, DecreaseMapSize);
        }

        private void IncreaseMapSize()
        {
            _currentSize = Mathf.Min(_currentSize + 1, MaxSize);
            _view.DrawSize(_currentSize);
            UpdateEnableControlls();
        }

        private void DecreaseMapSize()
        {
            _currentSize = Mathf.Max(_currentSize - 1, MinSize);
            _view.DrawSize(_currentSize);
            UpdateEnableControlls();
        }

        private void UpdateEnableControlls()
        {
            _inputController.SetButtonInteractable(EInputButtonType.IncreaseMapSizeButton, _currentSize < MaxSize);
            _inputController.SetButtonInteractable(EInputButtonType.DecreaseMapSizeButton, _currentSize > MinSize);
        }

        public void BuildMap()
        {
            if (_mapParent != null)
                GameObject.Destroy(_mapParent);

            var worldScreenY = _currentSize * (float)Screen.width / (float)Screen.height;

            var map = _mapGenerator.GenerateMap((int)worldScreenY, (int)_currentSize);
            _mapParent = _mapGraphicsBuilder.Build(map.GetCells());

            var camX = worldScreenY / 2f - 0.5f;
            Camera.main.transform.position = new Vector3(camX, _currentSize, 3f);

            OnMapBuilded?.Invoke(map);
        }

        public void Dispose()
        {
            OnMapBuilded = null;
        }
    }
}
