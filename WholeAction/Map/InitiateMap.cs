using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
namespace ns
{
    ///<summary>
    ///
    ///<summary>
    public class InitiateMap : MonoBehaviour
    {
        [ReadOnly(true)]
        public int mapW = 7;
        [ReadOnly(true)]
        public int mapH =7;
        [ReadOnly(true)]
        public bool isMoveRound = false;
        [ReadOnly(true)]
        public bool isRotateRound = false;

        public GameObject mapPre;
        public GameObject enemy;
        public Sprite barrierSprite;
        public Sprite barrierMoveTishi;
        public Sprite initiateSprite;
        public Sprite basicSprite;
        public Sprite lubanSprite;

        private List<List<Transform>> squareAssemble;
        private GameObject mapAssemble;
        private GameObject enemyAssemble;
        private List<AstarNote> pathList;
        private Sprite changeSprite;
        void Start ()
        {
            squareAssemble = new List<List<Transform>>();
            mapAssemble = GameObject.Find("mapAssemble");
            enemyAssemble = GameObject.Find("enemyAssemble");

            AstarManager.GetInstance().InitMap(mapW, mapH);
            CreatSquare();
            GetPath();

            foreach (Transform t in mapAssemble.transform)
            {
                changeSprite = basicSprite;
                if (t != mapAssemble.transform)
                {
                    if (ChargeStop(t))
                    {
                        changeSprite = barrierSprite;
                    }
                    else if (TransformNote(t).type == NoteType.initiate)
                    {
                        changeSprite = initiateSprite;
                    }
                    else if (TransformNote(t).type == NoteType.luban)
                    {
                        changeSprite = lubanSprite;
                    }
                    MouseChoose.GetInstance().ChageSprite(t, changeSprite);
                }
            }
        }

        private void Update()
        {
            MoveBarrier();
        }

        void CreatSquare()
        {
            for (int i = 0; i < mapW; i++)
            {
                List<Transform> row = new List<Transform>();
                for (int j = 0; j < mapH; j++)
                {
                    GameObject mapSquare = Instantiate(mapPre);
                    mapSquare.transform.position = new Vector3(i-1+0.01f*i , j+4+0.01f*j, 0);
                    mapSquare.transform.SetParent(mapAssemble.transform, true);
                    mapSquare.name = i.ToSafeString() + "_" + j.ToSafeString();
                    mapSquare.tag = "MapSquare";
                    row.Add(mapSquare.transform);
                }
                squareAssemble.Add(row);
            }
            mapAssemble.transform.rotation = Quaternion.Euler(40, 0, 50);
            mapAssemble.transform.position =new Vector3(9,-1,0);
            mapAssemble.transform.localScale = new Vector3(1.3f, 1.3f, 1);
        }
        private void GetPath()
        {
            pathList =AstarManager.GetInstance().FindPath(new Vector2(3,2),new Vector2(1,5));
            for (int i = 0;i < pathList.Count;i++)
            {
                Debug.Log(pathList[i].x.ToString()+ pathList[i].y.ToString());
            }
        }
        public void InitiateEnemy(Vector2 enemyLocation)
        {
            int x = Mathf.FloorToInt(enemyLocation.x);
            int y = Mathf.FloorToInt(enemyLocation.y);
            GameObject mapLocation = GameObject.Find(x.ToString()+"_"+ y.ToString());
            GameObject enemyGameObject = Instantiate(enemy);
            enemyGameObject.transform.position = mapLocation.transform.position;
            enemyGameObject.transform.SetParent(mapLocation.transform, true);
            enemyGameObject.tag = "Enemy";
        }
         private AstarNote TransformNote(Transform square)
        {
            if (square == null) { return null; }
            else 
            {
                string[] squareName = square.gameObject.name.Split('_');
                int x = int.Parse(squareName[0]);
                int y = int.Parse(squareName[1]);
                return AstarManager.GetInstance().mapNotes[x, y];
            }
        }


        private void MoveBarrier()
        {
            Transform barrier = MouseChoose.GetInstance().GetHitTransform("MapSquare");
            if (barrier != null && ChargeStop(barrier))
            {
                ChooseMapSquare(barrier);
            }
        }
        private void ChooseMapSquare(Transform barrier)
        {
            for (int i = 0; i<squareAssemble.Count; i++)
            {
                foreach (Transform square in squareAssemble[i])
                {
                       if (!ChargeStop(square)&& TransformNote(square).type!=NoteType.initiate && TransformNote(square).type != NoteType.luban)
                       {
                            MouseChoose.GetInstance().ChageSprite(square, barrierMoveTishi);
                       }
                 }
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                Transform newBarrier = MouseChoose.GetInstance().GetHitTransform("MapSquare");
                Sprite newBarrierSprite = newBarrier.GetComponent<Sprite>();
                if (newBarrierSprite == barrierMoveTishi)
                {
                    TransformNote(newBarrier).type = NoteType.stop;
                    TransformNote(barrier).type = NoteType.walk;
                }
            }
        }

        private bool ChargeStop(Transform square)
        {
            if (TransformNote(square).type == NoteType.stop)
            { return true; }
            else { return false; }
        }

        public void RotateMap(bool direction , int circle, int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                switch (circle)
                {
                    case 0:
                        RotateMethod(0, 0, mapH - 1, mapW - 1, direction);
                        ReSetInitiate();
                        break;
                    case 1:
                        RotateMethod(1, 1, mapH - 2, mapW - 2, direction);
                        break;
                    case 2:
                        RotateMethod(2, 2, mapH - 3, mapW - 3, direction);
                        break;
                }
            }
        }

        private void ReSetInitiate()
        {
            for (int i = 0; i < mapW; i++)
            {
                for (int j = 0; j < mapH; j++)
                {
                    if (TransformNote(squareAssemble[i][j]).type == NoteType.initiate)
                    {
                        TransformNote(squareAssemble[i][j]).type = NoteType.walk;
                    }
                    if ((i == 0 && j == mapH - 1) || (i == 0 && j == 0) || (i == mapW - 1 && j == mapH - 1) || (i == mapW - 1 && j == 0))
                    { TransformNote(squareAssemble[i][j]).type = NoteType.initiate; }
                }
            }
        }

        private void RotateMethod(int minX , int minY,int maxX, int maxY,bool direction )
        {
            
            Transform temp01 = squareAssemble[minX][minY];
            Transform temp02 = squareAssemble[maxX][maxY];
            Transform temp03 = squareAssemble[maxX][minY];
            Transform temp04 = squareAssemble[minX][maxY];
            Vector3 temp1 = temp03.position;
            Vector3 temp2 =temp04.position;
            Vector3 temp3 = temp01.position;
            Vector3 temp4 = temp02.position;
            
            if (!direction)
            {
                XRotateMethod(minX, minY, maxX, maxY,temp1,temp2);
                YRotateMethod(minX, minY, maxX,maxY,temp3,temp4);
                XTransformRotate(minX, minY, maxX, maxY,temp01,temp02);
                YTransformRotate(minX, minY, maxX,  maxY,temp03,temp04);
            }
            else
            {
                XRotateMethod(minX, maxY, maxX, minY,temp4,temp3);
                YRotateMethod(maxX, minY, minX, maxY, temp1, temp2);
                XTransformRotate(minX, maxY, maxX, minY,temp04,temp03);
                YTransformRotate(maxX, minY, minX, maxY,temp01,temp02);
            }
            
        }

        private void XRotateMethod(int minX, int minY, int maxX, int maxY, Vector3 temp1,Vector3 temp2)
        {
            int primX = minX;  int primXm = maxX; 
            while (minX < maxX - 1)
            {
                squareAssemble[minX][minY].position = squareAssemble[minX + 1][minY].position;
                minX++;
            }
            squareAssemble[minX][minY].position = temp1;
            minX = primX; maxX = primXm;
            while (minX + 1 < maxX)
            {
                squareAssemble[maxX][maxY].position = squareAssemble[maxX - 1][maxY].position;
                maxX--;
            }
            squareAssemble[maxX][maxY].position = temp2;
        }

        private void XTransformRotate( int minX, int minY,  int maxX, int maxY, Transform temp01, Transform temp02)
        {
            int primX = minX; int primXm = maxX;
            while (minX < maxX - 1)
            {
                squareAssemble[maxX][minY] = squareAssemble[maxX - 1][minY];
                maxX--;
            }
            squareAssemble[maxX][minY] = temp01;
            minX = primX; maxX = primXm;
            while (minX + 1 < maxX)
            {
                squareAssemble[minX][maxY] = squareAssemble[minX + 1][maxY];
                minX++;
            }
            squareAssemble[minX][maxY] = temp02;
        }

        private void YRotateMethod(int minX, int minY, int maxX, int maxY, Vector3 temp3, Vector3 temp4)
        {
           int primY = minY;  int primYm = maxY;
            while (minY < maxY - 1)
            {
                squareAssemble[maxX][minY].position = squareAssemble[maxX][minY + 1].position;
                minY++;
            }
            squareAssemble[maxX][minY].position = temp4;
            minY = primY; maxY = primYm;
            while (minY + 1 < maxY)
            {
                squareAssemble[minX][maxY].position = squareAssemble[minX][maxY - 1].position;
                maxY--;
            }
            squareAssemble[minX][maxY].position = temp3;
        }

        private void YTransformRotate(int minX,  int minY, int maxX,  int maxY,Transform temp03 , Transform temp04)
        {
            int primY = minY; int primYm = maxY;
            while (minY < maxY - 1)
            {
                squareAssemble[maxX][maxY] = squareAssemble[maxX][maxY - 1];
                maxY--;
            }
            squareAssemble[maxX][maxY] = temp03;
            minY = primY; maxY = primYm;
            while (minY + 1 < maxY)
            {
                squareAssemble[minX][minY] = squareAssemble[minX][minY + 1];
                minY++;
            }
            squareAssemble[minX][minY] = temp04;
        }
    }
}

