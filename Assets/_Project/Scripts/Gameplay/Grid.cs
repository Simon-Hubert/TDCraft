using UnityEngine;

namespace Gameplay
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private Tile[][] _grid;
        [SerializeField] private int _size = 5;
        [SerializeField] private float _padding = 0;
        [SerializeField] private Tile prefabTest;
        [SerializeField] private GameObject prefabTESTSNAP;

        private Vector2 _gridSize = new Vector2();

        private void OnValidate()
        {
            if (_size % 2 == 0)
            {
                _size = _size + 1;
            }
        }

        private void Awake()
        {
            _grid = new Tile[_size][];
            float x0 = (transform.position.x - transform.localScale.x / 2) + _padding;
            float y0 = (transform.position.y - transform.localScale.y / 2) + _padding;
            float x1 = (transform.position.x + transform.localScale.x / 2) - _padding;
            float y1 = (transform.position.y + transform.localScale.y / 2) - _padding;

            float sizex = (x1 - x0) / _size;
            float sizey = (y1 - y0) / _size;
            _gridSize = new Vector2(sizex, sizey);
            for (int i = 0; i < _size; i++)
            {
                _grid[i] = new Tile[_size];
                for (int j = 0; j < _size; j++)
                {
                    _grid[i][j] = Instantiate(prefabTest, new Vector3(x0 + sizex/2 +  i * sizex, y0 + sizey/2 + j * sizey, 0), Quaternion.identity);
                    _grid[i][j].SetPos(new Vector2(x0 + sizex/2 +  i * sizex, y0 + sizey/2 + j * sizey));
                    Debug.Log(_grid[i][j].Pos);
                }
            }
        }

        [ContextMenu("SNAP TEST")]
        public void SNAPSTEST()
        {
            SnapToGrid(prefabTESTSNAP);
        }
        public void SnapToGrid(GameObject target)
        {
            target.transform.position = GetClosestTile(target.transform.position);
        }

        Vector3 GetClosestTile(Vector3 position)
        {
            Vector3 closestTile = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    if (Vector3.Distance(position, closestTile) > Vector3.Distance(position, _grid[i][j].Pos))
                    {
                        Debug.Log("closestTiledist : " + Vector3.Distance(position, closestTile) + " newPosdist : " + Vector3.Distance(position, _grid[i][j].Pos));
                        closestTile = _grid[i][j].Pos; 
                    }
                    else Debug.Log("TRY closestTiledist : " + Vector3.Distance(position, closestTile) + " newPosdist : " + Vector3.Distance(position, _grid[i][j].Pos));;
                }
            }

            return closestTile;
        }

        private void OnDrawGizmos()
        {
            float sizeY = 2 * ((transform.position.y - transform.localScale.y / 2) + 
                               (transform.position.y + transform.localScale.y)) / _size;
            for (int i = 0; i < _size; i++)
            {
                Vector2 start = new Vector2((transform.position.x - transform.localScale.x/2) + _padding, transform.position.y - (transform.localScale.y/2) + _padding + i *  sizeY);
                Vector2 end = new Vector2((transform.position.x + transform.localScale.x/2) - _padding, transform.position.y - (transform.localScale.y/2) + _padding + i * sizeY);
                Gizmos.DrawLine(start, end);
            }

            float sizeX = 2 * ((transform.position.x - transform.localScale.x / 2) +
                               (transform.position.x + transform.localScale.x)) / _size;
            for (int i = 0; i < _size; i++)
            {
                Vector2 start = new Vector2((transform.position.x - transform.localScale.x/2) + _padding + i * sizeX, transform.position.y - (transform.localScale.y/2) + _padding);
                Vector2 end = new Vector2((transform.position.x - transform.localScale.x/2) + _padding + i * sizeX, transform.position.y + (transform.localScale.y/2) - _padding);
                Gizmos.DrawLine(start, end);
            }
        }
    }
}
