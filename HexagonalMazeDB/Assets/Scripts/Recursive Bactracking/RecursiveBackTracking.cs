using Assets.Scripts.Hexa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveBackTracking : MonoBehaviour
{
    private int numCellsVisited;
    private Stack<HexCoordinates> stack;
    private HexGrid hexGrid;
    private List<HexaDirection> visinhos;

    // Use this for initialization
    void Awake()
    {
        stack = new Stack<HexCoordinates>();
        hexGrid = GetComponent<HexGrid>();
        visinhos = new List<HexaDirection>();
        numCellsVisited = 0;
    }
    private void OnEnable()
    {
        stack.Push(new HexCoordinates(hexGrid.cells[0].coordinates.X, hexGrid.cells[0].coordinates.Z));
        hexGrid.cells[0].visited = true;
        numCellsVisited++;

        
        while (numCellsVisited < hexGrid.width * hexGrid.height)
        {
            Debug.Log("X-> " + stack.Peek().X + "Z-> " + stack.Peek().Z);
            //Bounds
            if (stack.Peek().Z > 0 && (stack.Peek().X < hexGrid.width - 1 || stack.Peek().Z % 2 == 0))
            {
                //SouthEast -> se vais para south east e estas no offset (z%2!=0) entao tens que andar tambem uma celula para a direita
                if (stack.Peek().X < hexGrid.width - 1 && stack.Peek().Z % 2 == 0)
                {
                    if (hexGrid.cells[OffSetFunction(0, -1)].visited == false)
                    {
                        visinhos.Add(HexaDirection.SE);
                    }
                }
                else if (stack.Peek().X < hexGrid.width - 1 && stack.Peek().Z % 2 != 0)
                {
                    if (hexGrid.cells[OffSetFunction(1, -1)].visited == false)
                    {
                        visinhos.Add(HexaDirection.SE);
                    }
                }
            }
            if (stack.Peek().Z > 0 && (stack.Peek().X > 0 || stack.Peek().Z % 2 != 0))
            {
                if (stack.Peek().X < hexGrid.width - 1 && stack.Peek().Z % 2 == 0)
                {
                    //SouthWest
                    if (hexGrid.cells[OffSetFunction(0, -1)].visited == false)
                    {
                        visinhos.Add(HexaDirection.SW);
                    }
                }
                else if (stack.Peek().X == hexGrid.width - 1 && stack.Peek().Z % 2 != 0)
                {
                    if (hexGrid.cells[OffSetFunction(-1, -1)].visited == false)
                    {
                        visinhos.Add(HexaDirection.SW);
                    }
                }
            }
            if (stack.Peek().X < hexGrid.width - 1)
            {
                //East
                if (hexGrid.cells[OffSetFunction(1, 0)].visited == false)
                {
                    visinhos.Add(HexaDirection.E);
                }
            }
            if (stack.Peek().Z < hexGrid.height - 1 && (stack.Peek().X < hexGrid.width - 1 || stack.Peek().Z % 2 == 0))
            {
                if (stack.Peek().X >= 0 && stack.Peek().Z % 2 == 0)
                {
                    //NorthEast
                    if (hexGrid.cells[OffSetFunction(0, 1)].visited == false)
                    {
                        visinhos.Add(HexaDirection.NE);
                    }
                }
                else if (stack.Peek().X >= 0 && stack.Peek().Z % 2 != 0)
                {
                    //NorthEast
                    if (hexGrid.cells[OffSetFunction(1, 1)].visited == false)
                    {
                        visinhos.Add(HexaDirection.NE);
                    }
                }
                
            }
            if (stack.Peek().Z < hexGrid.height - 1 && (stack.Peek().X > 0 || stack.Peek().Z % 2 != 0))
            {
                if (stack.Peek().X < hexGrid.width - 1 && stack.Peek().Z % 2 != 0)
                {
                    //NorthWest
                    if (hexGrid.cells[OffSetFunction(0, 1)].visited == false)
                    {
                        visinhos.Add(HexaDirection.NW);
                    }
                }
                else if (stack.Peek().X == hexGrid.width - 1 && stack.Peek().Z % 2 == 0)
                {
                    if (hexGrid.cells[OffSetFunction(-1, 1)].visited == false)
                    {
                        visinhos.Add(HexaDirection.NW);
                    }
                }
                else if (stack.Peek().X == hexGrid.width - 1 && stack.Peek().Z % 2 != 0)
                {
                    if (hexGrid.cells[OffSetFunction(0, 1)].visited == false)
                    {
                        visinhos.Add(HexaDirection.NW);
                    }
                }
            }
            if (stack.Peek().X > 0)
            {
                //West
                if (hexGrid.cells[OffSetFunction(-1, 0)].visited == false)
                {
                    visinhos.Add(HexaDirection.W);
                }
            }
            if (visinhos.Count > 0)
            {
                foreach (var item in visinhos)
                {
                    Debug.Log("visinho " + item + " na contagem " + numCellsVisited);
                }
                int number = (int)(Random.value * 100);
                HexaDirection proximaDir = visinhos[number % visinhos.Count];
                Debug.Log("proxima direction ->" + proximaDir);
                switch (proximaDir)
                {
                    //NorthEast
                    case HexaDirection.NE:
                        if (stack.Peek().X >= 0 && stack.Peek().Z % 2 == 0)
                        {
                            hexGrid.cells[OffSetFunction(0, 0)].hexaDirections.Add(HexaDirection.NE);
                            hexGrid.cells[OffSetFunction(0, 1)].hexaDirections.Add(HexaDirection.SW);
                            stack.Push(new HexCoordinates(stack.Peek().X + 0, stack.Peek().Z + 1));
                        }
                        else if (stack.Peek().X >= 0 && stack.Peek().Z % 2 != 0)
                        {
                            hexGrid.cells[OffSetFunction(0, 0)].hexaDirections.Add(HexaDirection.NE);
                            hexGrid.cells[OffSetFunction(1, 1)].hexaDirections.Add(HexaDirection.SW);
                            stack.Push(new HexCoordinates(stack.Peek().X + 1, stack.Peek().Z + 1));
                        }
                        break;
                    //East
                    case HexaDirection.E:
                        hexGrid.cells[OffSetFunction(0, 0)].hexaDirections.Add(HexaDirection.E);
                        hexGrid.cells[OffSetFunction(1, 0)].hexaDirections.Add(HexaDirection.W);
                        stack.Push(new HexCoordinates(stack.Peek().X + 1, stack.Peek().Z + 0));
                        break;
                    //SouthEast
                    case HexaDirection.SE:
                        if (stack.Peek().X < hexGrid.width - 1 && stack.Peek().Z % 2 == 0)
                        {
                            hexGrid.cells[OffSetFunction(0, 0)].hexaDirections.Add(HexaDirection.SE);
                            hexGrid.cells[OffSetFunction(0, -1)].hexaDirections.Add(HexaDirection.NW);
                            stack.Push(new HexCoordinates(stack.Peek().X + 0, stack.Peek().Z - 1));
                        }
                        else if (stack.Peek().X < hexGrid.width - 1 && stack.Peek().Z % 2 != 0)
                        {
                            hexGrid.cells[OffSetFunction(0, 0)].hexaDirections.Add(HexaDirection.SE);
                            hexGrid.cells[OffSetFunction(1, -1)].hexaDirections.Add(HexaDirection.NW);
                            stack.Push(new HexCoordinates(stack.Peek().X + 1, stack.Peek().Z - 1));
                        }
                        break;
                    //West
                    case HexaDirection.W:
                        hexGrid.cells[OffSetFunction(0, 0)].hexaDirections.Add(HexaDirection.W);
                        hexGrid.cells[OffSetFunction(-1, 0)].hexaDirections.Add(HexaDirection.E);
                        stack.Push(new HexCoordinates(stack.Peek().X - 1, stack.Peek().Z + 0));
                        break;
                    //NorthWest
                    case HexaDirection.NW:
                        if (stack.Peek().X < hexGrid.width - 1)
                        {
                            hexGrid.cells[OffSetFunction(0, 0)].hexaDirections.Add(HexaDirection.NW);
                            hexGrid.cells[OffSetFunction(0, 1)].hexaDirections.Add(HexaDirection.SE);
                            stack.Push(new HexCoordinates(stack.Peek().X + 0, stack.Peek().Z + 1));
                        }
                        else if (stack.Peek().X == hexGrid.width - 1 && stack.Peek().Z % 2 != 0)
                        {
                            hexGrid.cells[OffSetFunction(0, 0)].hexaDirections.Add(HexaDirection.NW);
                            hexGrid.cells[OffSetFunction(0, 1)].hexaDirections.Add(HexaDirection.SE);
                            stack.Push(new HexCoordinates(stack.Peek().X + 0, stack.Peek().Z + 1));
                        }
                        else if (stack.Peek().X == hexGrid.width - 1 && stack.Peek().Z % 2 == 0)
                        {
                            hexGrid.cells[OffSetFunction(0, 0)].hexaDirections.Add(HexaDirection.NW);
                            hexGrid.cells[OffSetFunction(-1, 1)].hexaDirections.Add(HexaDirection.SE);
                            stack.Push(new HexCoordinates(stack.Peek().X + -1, stack.Peek().Z + 1));
                        }
                        break;
                    //SouthWest
                    case HexaDirection.SW:
                        if (stack.Peek().X < hexGrid.width - 1 && stack.Peek().Z % 2 == 0)
                        {
                            hexGrid.cells[OffSetFunction(0, 0)].hexaDirections.Add(HexaDirection.SW);
                            hexGrid.cells[OffSetFunction(0, -1)].hexaDirections.Add(HexaDirection.NE);
                            stack.Push(new HexCoordinates(stack.Peek().X + 0, stack.Peek().Z - 1));
                        }
                        else if (stack.Peek().X == hexGrid.width - 1 && stack.Peek().Z % 2 != 0)
                        {
                            hexGrid.cells[OffSetFunction(0, 0)].hexaDirections.Add(HexaDirection.SW);
                            hexGrid.cells[OffSetFunction(0, -1)].hexaDirections.Add(HexaDirection.NE);
                            stack.Push(new HexCoordinates(stack.Peek().X + -1, stack.Peek().Z - 1));
                        }
                        break;
                }
                //New cell
                hexGrid.cells[OffSetFunction(0, 0)].visited = true;
                numCellsVisited++;
            }
            else
            {
                stack.Pop();
            }
            visinhos.Clear();
        }
        //hexGrid.cells[hexGrid.width / 2].hexaDirections.Add(HexaDirection.SW);
        hexGrid.cells[hexGrid.width - 1].hexaDirections.Add(HexaDirection.E);
        hexGrid.cells[0].hexaDirections.Add(HexaDirection.W);
        hexGrid.cells[hexGrid.cells.Length - 1 - hexGrid.width / 2].hexaDirections.Add(HexaDirection.NE);
        hexGrid.cells[hexGrid.cells.Length  - hexGrid.width / 2].hexaDirections.Add(HexaDirection.NW);

    }

    private int OffSetFunction(int x, int z)
    {
        return (stack.Peek().Z + z) * hexGrid.width + (stack.Peek().X + x);
    }
}
