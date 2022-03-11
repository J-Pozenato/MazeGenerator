using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public class LaydownMazeTile : MonoBehaviour
{
    [System.Serializable]
    public class Cell
    {
        public bool visited = false;
        public bool top = true;
        public bool bottom = true;
        public bool left = true;
        public bool right = true;
        public int nCount = 4;

        // public Neighbor[] neight;
        public Neighbor neight;


    }

    [System.Serializable]
    public class Neighbor
    {
        //public int nIndex = 0;
        //public char nSide = ' ';
        public bool ntop = true;
        public bool nbottom = true;
        public bool nleft = true;
        public bool nright = true;
    }


    public float speed = 0.5f;

    [Range(1, 50)]
    public int height = 0;
    [Range(1, 50)]
    public int width = 0;

    public int[,] size;
    public Vector2 moving = new Vector2();
    public Vector2 target = new Vector2(0, 0);

    public Button button;
    public GameObject mazeTile;
    public GameObject floor;
    public GameObject ui;
    public Camera cam;
    public GameObject destroyer;
    public Cell[] cells;


    //public Collider2D col;
    // public LaydownMazeTile tile;

    // Start is called before the first frame update
    void Start()
    {
        //cam = GetComponent<Camera>();
        // col = GetComponent<Collider2D>();
        //destroyer = GetComponent<GameObject>();
        Button btn = button.GetComponent<Button>();
        moving.x = moving.y = 0;
        btn.onClick.AddListener(layMaze);
    }

    public void layMaze()
    {
        ui.gameObject.SetActive(false);
        size = new int[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Instantiate(mazeTile, new Vector3(j, i, 0), Quaternion.identity);
                Instantiate(floor, new Vector3(j, i, 0), Quaternion.identity);
            }
        }

        cam.transform.position = new Vector3(width / 2, height / 2, -10);
        cam.orthographicSize = 2 + height / 2;

        createCells();

        StartCoroutine(Generate());



    }

    public void createCells()
    {
        cells = new Cell[height * width];

        for (int cellprocess = 0; cellprocess < cells.Length; cellprocess++)
        {
            cells[cellprocess] = new Cell();
            cells[cellprocess].neight = new Neighbor();


            //cells[cellprocess].neight = new Neighbor[4];

            //cells[cellprocess].neight[0] = new Neighbor();
            //cells[cellprocess].neight[0].nIndex = cellprocess - 1;
            //cells[cellprocess].neight[0].nSide = 'L';

            //cells[cellprocess].neight[1] = new Neighbor();
            //cells[cellprocess].neight[1].nIndex = cellprocess + 1;
            //cells[cellprocess].neight[1].nSide = 'R';

            //cells[cellprocess].neight[2] = new Neighbor();
            //cells[cellprocess].neight[2].nIndex = cellprocess - width;
            //cells[cellprocess].neight[2].nSide = 'B';

            //cells[cellprocess].neight[3] = new Neighbor();
            //cells[cellprocess].neight[3].nIndex = cellprocess + width;
            //cells[cellprocess].neight[3].nSide = 'T';
        }

        for (int i = 0; i < width; i++)
        {
            cells[i].bottom = false;
            cells[i].nCount--;
            cells[i].neight.nbottom = false;
            //cells[i].neight[2] = null;
        }
        for (int i = 0; i <= (width * (height - 1)); i += width)
        {
            cells[i].left = false;
            cells[i].nCount--;
            cells[i].neight.nleft = false;
            //cells[i].neight[0] = null;
        }
        for (int i = (width * (height - 1)); i < (width * height); i++)
        {
            cells[i].top = false;
            cells[i].nCount--;
            cells[i].neight.ntop = false;
            //cells[i].neight[3] = null;
        }
        for (int i = width - 1; i < (width * height); i += width)
        {
            cells[i].right = false;
            cells[i].nCount--;
            cells[i].neight.nright = false;
            //cells[i].neight[1] = null;
        }



    }


    IEnumerator Generate()
    {
        int currentCell = 0;
        int lastCell = 0;
        int test = 1;
        Debug.Log("gerando");
        moving.x = moving.y = 0;
        int visitedCount = 1;
        cells[currentCell].visited = true;
        int ncount;

        while (visitedCount < height * width)
        {
            ncount = cells[currentCell].nCount;
            Vector2 lastTarget = new Vector2(0, 0);

            if (ncount != 0)
            {
                int random = UnityEngine.Random.Range(0, ncount);
                if (random == 0)
                {
                    if (cells[currentCell].top && !cells[currentCell + width].visited && (cells[currentCell].bottom || cells[currentCell].left || cells[currentCell].right))
                    {
                        lastTarget = target;
                        target += new Vector2(0, 1);
                        yield return new WaitForSeconds(2);
                        moving.y = 0;
                        cells[currentCell].top = false;
                        cells[(currentCell + width)].bottom = false;
                        lastCell = currentCell;
                        currentCell += width;
                    }
                    else if (cells[currentCell].right && !cells[currentCell + 1].visited && (cells[currentCell].bottom || cells[currentCell].left || cells[currentCell].top))
                    {
                        lastTarget = target;
                        target += new Vector2(1, 0);
                        yield return new WaitForSeconds(2);
                        moving.x = 0;
                        cells[currentCell].right = false;
                        cells[(currentCell + 1)].left = false;
                        lastCell = currentCell;
                        currentCell += 1;
                    }
                    else if (cells[currentCell].bottom && !cells[currentCell - width].visited && (cells[currentCell].top || cells[currentCell].left || cells[currentCell].right))
                    {
                        lastTarget = target;
                        target += new Vector2(0, -1);
                        yield return new WaitForSeconds(2);
                        moving.y = 0;
                        cells[currentCell].bottom = false;
                        cells[(currentCell - width)].top = false;
                        lastCell = currentCell;
                        currentCell -= width;
                    }
                    else if (cells[currentCell].left && !cells[currentCell - 1].visited && (cells[currentCell].bottom || cells[currentCell].right || cells[currentCell].top))
                    {
                        lastTarget = target;
                        target += new Vector2(-1, 0);
                        yield return new WaitForSeconds(2);
                        moving.x = 0;
                        cells[currentCell].left = false;
                        cells[(currentCell - 1)].right = false;
                        lastCell = currentCell;
                        currentCell -= 1;
                    }
                    else
                    {


                    }
                }
                else if (random == 1)
                {
                    if (cells[currentCell].bottom && !cells[currentCell - width].visited && (cells[currentCell].top || cells[currentCell].left || cells[currentCell].right))
                    {
                        lastTarget = target;
                        target += new Vector2(0, -1);
                        yield return new WaitForSeconds(2);
                        moving.y = 0;
                        cells[currentCell].bottom = false;
                        cells[(currentCell - width)].top = false;
                        lastCell = currentCell;
                        currentCell -= width;
                    }
                    else if (cells[currentCell].left && !cells[currentCell - 1].visited && (cells[currentCell].bottom || cells[currentCell].right || cells[currentCell].top))
                    {
                        lastTarget = target;
                        target += new Vector2(-1, 0);
                        yield return new WaitForSeconds(2);
                        moving.x = 0;
                        cells[currentCell].left = false;
                        cells[(currentCell - 1)].right = false;
                        lastCell = currentCell;
                        currentCell -= 1;
                    }
                    else if (cells[currentCell].top && !cells[currentCell + width].visited && (cells[currentCell].bottom || cells[currentCell].left || cells[currentCell].right))
                    {
                        lastTarget = target;
                        target += new Vector2(0, 1);
                        yield return new WaitForSeconds(2);
                        moving.y = 0;
                        cells[currentCell].top = false;
                        cells[(currentCell + width)].bottom = false;
                        lastCell = currentCell;
                        currentCell += width;
                    }
                    else if (cells[currentCell].right && !cells[currentCell + 1].visited && (cells[currentCell].bottom || cells[currentCell].left || cells[currentCell].top))
                    {
                        lastTarget = target;
                        target += new Vector2(1, 0);
                        yield return new WaitForSeconds(2);
                        moving.x = 0;
                        cells[currentCell].right = false;
                        cells[(currentCell + 1)].left = false;
                        lastCell = currentCell;
                        currentCell += 1;
                    }
                    else
                    {
                        target = lastTarget;
                        currentCell = lastCell;
                    }
                }
                else if (random == 2)
                {

                    if (cells[currentCell].right && !cells[currentCell + 1].visited && (cells[currentCell].bottom || cells[currentCell].left || cells[currentCell].top))
                    {
                        lastTarget = target;
                        target += new Vector2(1, 0);
                        yield return new WaitForSeconds(2);
                        moving.x = 0;
                        cells[currentCell].right = false;
                        cells[(currentCell + 1)].left = false;
                        lastCell = currentCell;
                        currentCell += 1;
                    }
                    else if (cells[currentCell].top && !cells[currentCell + width].visited && (cells[currentCell].bottom || cells[currentCell].left || cells[currentCell].right))
                    {
                        lastTarget = target;
                        target += new Vector2(0, 1);
                        yield return new WaitForSeconds(2);
                        moving.y = 0;
                        cells[currentCell].top = false;
                        cells[(currentCell + width)].bottom = false;
                        lastCell = currentCell;
                        currentCell += width;
                    }
                    else if (cells[currentCell].left && !cells[currentCell - 1].visited && (cells[currentCell].bottom || cells[currentCell].right || cells[currentCell].top))
                    {
                        lastTarget = target;
                        target += new Vector2(-1, 0);
                        yield return new WaitForSeconds(2);
                        moving.x = 0;
                        cells[currentCell].left = false;
                        cells[(currentCell - 1)].right = false;
                        lastCell = currentCell;
                        currentCell -= 1;
                    }
                    else if (cells[currentCell].bottom && !cells[currentCell - width].visited && (cells[currentCell].top || cells[currentCell].left || cells[currentCell].right))
                    {
                        lastTarget = target;
                        target += new Vector2(0, -1);
                        yield return new WaitForSeconds(2);
                        moving.y = 0;
                        cells[currentCell].bottom = false;
                        cells[(currentCell - width)].top = false;
                        lastCell = currentCell;
                        currentCell -= width;
                    }
                    else
                    {
                        target = lastTarget;
                        currentCell = lastCell;
                    }
                }
                else if (random == 3)
                {
                    if (cells[currentCell].left && !cells[currentCell - 1].visited && (cells[currentCell].bottom || cells[currentCell].right || cells[currentCell].top))
                    {
                        lastTarget = target;
                        target += new Vector2(-1, 0);
                        yield return new WaitForSeconds(2);
                        moving.x = 0;
                        cells[currentCell].left = false;
                        cells[(currentCell - 1)].right = false;
                        lastCell = currentCell;
                        currentCell -= 1;
                    }
                    else if (cells[currentCell].bottom && !cells[currentCell - width].visited && (cells[currentCell].top || cells[currentCell].left || cells[currentCell].right))
                    {
                        lastTarget = target;
                        target += new Vector2(0, -1);
                        yield return new WaitForSeconds(2);
                        moving.y = 0;
                        cells[currentCell].bottom = false;
                        cells[(currentCell - width)].top = false;
                        lastCell = currentCell;
                        currentCell -= width;
                    }
                    else if (cells[currentCell].right && !cells[currentCell + 1].visited && (cells[currentCell].bottom || cells[currentCell].left || cells[currentCell].top))
                    {
                        lastTarget = target;
                        target += new Vector2(1, 0);
                        yield return new WaitForSeconds(2);
                        moving.x = 0;
                        cells[currentCell].right = false;
                        cells[(currentCell + 1)].left = false;
                        lastCell = currentCell;
                        currentCell += 1;
                    }
                    else if (cells[currentCell].top && !cells[currentCell + width].visited && (cells[currentCell].bottom || cells[currentCell].left || cells[currentCell].right))
                    {
                        lastTarget = target;
                        target += new Vector2(0, 1);
                        yield return new WaitForSeconds(2);
                        moving.y = 0;
                        cells[currentCell].top = false;
                        cells[(currentCell + width)].bottom = false;
                        lastCell = currentCell;
                        currentCell += width;
                    }
                    else
                    {
                        target = lastTarget;
                        currentCell = lastCell;
                    }
                }
                test++;
                visitedCount++;
                cells[currentCell].visited = true;
            }
            else
            {
                Debug.Log("error");
            }


        }
        yield return null;
    }

    private void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }




    public void ReadHeight(string s)
    {
        height = int.Parse(s);

    }
    public void ReadWidht(string s)
    {
        width = int.Parse(s);

    }
}

