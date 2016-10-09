using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;
using System.Collections.Generic;
using System;

/// <summary>
/// manager all measure tools here
/// </summary>
public class MeasureManager : Singleton<MeasureManager>
{
    private IGeometry manager;
    public GeometryMode mode;
    

    // set up prefabs
    public GameObject LinePrefab;
    public GameObject PointPrefab;
    public GameObject ModeTipObject;
    public GameObject TextPrefab;


    void Start()
    {
        // inti measure mode
        switch (mode)
        {

            case GeometryMode.Ploygon:
                manager = PolygonManager.Instance;
                break;
            case GeometryMode.Line:
                manager = LineManager.Instance;
                break;
            case GeometryMode.Origin:
                manager = OriginManager.Instance;
                break;
            default:
                manager = PointManager.Instance;
                break;
        }
    }

    // place spatial point
    public void OnSelect()
    {
        manager.AddPoint(LinePrefab, PointPrefab, TextPrefab);
    }

    // Global Data Structure
    
    static public class GDS
    {
        
        static int[,] arrLegoArray = new int[24,24];
        public static int[,] LegoArray
        {
            get
            {
                return GDS.arrLegoArray;
            }
        }

        // Assigning the IDs to LegoArray
        static public void ArrayAssign()
        {
            int z, x;
            int a = 0;
            for (z = 0; z < 24; z++)
            {
                for (x = 0; x < 24; x++)
                {
                    LegoArray[z, x] = a;
                    a++;
                }
            }
        }

        static Dictionary<int, string> _dict = new Dictionary<int, string>
    {
        {1, "entries"},
        {2, "images"},
        {3, "views"},
        {4, "files"},
        {5, "results"},
        {6, "words"},
        {7, "definitions"},
        {8, "items"},
        {9, "megabytes"},
        {10, "games"}
    };
        public static string GetDescription(int id)
        {
            // Try to get the result in the static Dictionary
            string result;
            if (_dict.TryGetValue(id, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static int getKey(string Value)
        {
            if (_dict.ContainsValue(Value))
            {
                var ListValueData = new List<string>();
                var ListKeyData = new List<int>();

                var Values = _dict.Values;
                var Keys = _dict.Keys;

                foreach (var item in Values)
                {
                    ListValueData.Add(item);
                }

                var ValueIndex = ListValueData.IndexOf(Value);

                foreach (var item in Keys)
                {
                    ListKeyData.Add(item);
                }

                return ListKeyData[ValueIndex];
            }

            return -1;
        }

        public static int GetObjID(int z, int x)
        {
            return LegoArray[z, x];
        }
    }

    // delete latest line or geometry
    public void DeleteLine()
    {
        manager.Delete();
    }

    // delete all lines or geometry
    public void ClearAll()
    {
        manager.Clear();
    }

    // if current mode is geometry mode, try to finish geometry
    public void OnPloygonClose()
    {
        IPolygonClosable client = PolygonManager.Instance;
        client.ClosePloygon(LinePrefab, TextPrefab);
    }

    // change measure mode
    public void OnLineMode()
    {
        try
        {
            manager.Reset();
            mode = GeometryMode.Line;
            manager = LineManager.Instance;

        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        ModeTipObject.SetActive(true);
    }

    public void OnPolyMode()
    {
        try
        {
            manager.Reset();
            mode = GeometryMode.Ploygon;
            manager = PolygonManager.Instance;

        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        ModeTipObject.SetActive(true);
    }

    public void OnPointMode()
    {
        try
        {
            manager.Reset();
            mode = GeometryMode.Point;
            manager = PointManager.Instance;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        ModeTipObject.SetActive(true);
    }

    public void OnOriginMode()
    {
        try
        {
            manager.Reset();
            mode = GeometryMode.Origin;
            manager = OriginManager.Instance;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        ModeTipObject.SetActive(true);
    }
}

public class Point
{
    public Vector3 Position { get; set; }
    public GameObject Root { get; set; }
    public bool IsStart { get; set; }
}

public class Origin
{
    public static Vector3 Point { get; set; }
}

public enum GeometryMode
{
    Point,
    Line,
    Triangle,
    Rectangle,
    Cube,
    Ploygon,
    Origin
}