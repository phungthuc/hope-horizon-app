using UnityEngine;

namespace FarmerEscape.Scripts.Map
{
    public class MapGenerator : MonoBehaviour
    {
        public GameObject tilePrefab;
        public int width;
        public int height;
        public float tileSize;
        public Vector3 origin;
        public Vector3 offset;
        public void GenerateMap()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var tile = Instantiate(tilePrefab, origin + new Vector3(x * tileSize, y * tileSize, 0) + offset, Quaternion.identity);
                    tile.transform.SetParent(transform);
                }
            }
        }
    }
}
