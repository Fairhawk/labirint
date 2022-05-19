using UnityEngine;
using UnityEngine.UI;

namespace World.MapBuildController.Views
{
    public class MapBuildControllerView : MonoBehaviour
    {
        [SerializeField] private Text _sizeText;

        public void DrawSize(int size) => _sizeText.text = $"SIZE: {size}".ToUpper();
    }
}
