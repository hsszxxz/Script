using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace ns
{
    ///<summary>
    ///
    ///<summary>
    public class AstarManager 
    {
        public static  AstarManager Instance;
        public static AstarManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new AstarManager();
            }
            return Instance;
        }

        private int mapW;
        private int mapH;

        public AstarNote[,] mapNotes;
         
        public List<AstarNote> openList = new List<AstarNote>();
        public List<AstarNote> closeList = new List<AstarNote>();

        public void InitMap(int w,int h)
        {
            mapNotes = new AstarNote[w,h];
            mapW = w;
            mapH = h;
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if ((i==0 && j ==h-1) || (i==0&& j ==0)|| (i == w-1 && j == h - 1) || (i == w-1 && j == 0))
                    { mapNotes[i, j] = new AstarNote(i, j, NoteType.initiate); }
                    else if (i ==(w-1)/2 && j == (h - 1) / 2)
                    { mapNotes[i, j] = new AstarNote(i, j, NoteType.luban); }
                    else { mapNotes[i, j] = new AstarNote(i, j, NoteType.walk);}
                }
            }
            openList.Clear();
            closeList.Clear();
        }
       public List<AstarNote> FindPath(Vector2 startPoint, Vector2 endPoint)
        {
            openList.Clear();
            closeList.Clear();

            int sx = Mathf.FloorToInt(startPoint.x);
            int ex = Mathf.FloorToInt(endPoint.x);
            int sy = Mathf.FloorToInt(startPoint.y);
            int ey = Mathf.FloorToInt(endPoint.y);
            if (isMapExternal(sx, ex) || isMapExternal(sy, ey))
            {
                Debug.Log("起点，终点出界");
                return null;
            }

            var startNote = mapNotes[sx, sy];
            var endNote = mapNotes[ex, ey];

            closeList.Add(startNote);

            if (startNote.type == NoteType.stop || endNote.type == NoteType.stop)
            {
                Debug.Log("起点，终点为阻挡点");
                return null;
            }

            bool secced =FindEndPoint(sx, sy, ex, ey);
            if (secced)
            {
                AstarNote pathEndNote = closeList[closeList.Count - 1];
                var pathList = new List<AstarNote>();
                var curNote = pathEndNote;
                while (true)
                {
                    pathList.Add(curNote);
                    if (curNote.father == null)
                    {
                        break;
                    }
                    curNote = curNote.father;
                }
                pathList.Reverse();
                return pathList;
            }
            return null;
        }

        private bool FindEndPoint(int sx, int sy, int ex, int ey)
        {
            var startNote = mapNotes[sx, sy];
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0)
                    { continue; }
                    int cx = sx + i;
                    int cy = sy + j;

                    if (cx ==ex && cy ==ey)
                    {
                        return true;
                    }
                    if (isMapExternal(cx, cy))
                    { continue; }

                    var curNote = mapNotes[cx, cy];

                    if (curNote.type == NoteType.stop)
                    { continue; }

                    if (openList.Contains(curNote) || closeList.Contains(curNote))
                    { continue; }

                    curNote.father = startNote;
                    float d = 1;
                    if (i != 0 && j != 0)
                    { d = 1.4f; }
                    float g = startNote.g + d;
                    ///
                    float h = Mathf.Abs(cx - ex) + Mathf.Abs(cy - ey);
                    ///
                    float f = g + h;

                    curNote.g = g;
                    curNote.h = h;
                    curNote.f = f;

                    openList.Add(curNote);
                }
            }

            if (openList .Count == 0)
            {
                Debug.Log("死路");
                return false;
            }
            openList.Sort(comparison: (note1, note2) =>
            {
                return note1.f >= note2.f ? 1 : -1;
            });

            var minNote = openList[0];

            closeList.Add(minNote);
            openList.RemoveAt(0);

            if (minNote.x == ex && minNote.y == ey)
            {
                return true;
            }

            return  FindEndPoint(minNote.x, minNote.y, ex, ey);
        }

        private bool isMapExternal(int x, int y)
        {
            if (x<0 || y < 0||x>=mapW ||y >=mapH)
                return true;
            else
                return false;
        }
    }
}

