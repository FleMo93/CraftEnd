using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CraftEnd.Engine.Physics
{
  public static class Layer
  {
    private static bool[,] collisionMatrix = new bool[0, 0];
    public static ReadOnlyDictionary<int, bool[]> CollisionMatrix;

    public static void SetCollision(int layerA, bool isColliding, params int[] layerNumbers)
    {
      int matrixLength = layerNumbers.Aggregate((a, b) => a > b ? a : b) + 1;

      if (collisionMatrix.GetLength(0) < matrixLength)
      {
        var newMatrix = new bool[matrixLength, matrixLength];
        for (var y = 0; y < collisionMatrix.GetLength(0); y++)
          for (var x = 0; x < collisionMatrix.GetLength(1); x++)
            newMatrix[y, x] = collisionMatrix[y, x];

        collisionMatrix = newMatrix;
      }

      foreach (var layerNumber in layerNumbers)
      {
        collisionMatrix[layerA, layerNumber] = isColliding;
        collisionMatrix[layerNumber, layerA] = isColliding;
      }

      var collisionDictionary = new Dictionary<int, bool[]>();
      for (var layerNumber = 0; layerNumber < matrixLength; layerNumber++)
      {
        var row = new bool[matrixLength];
        Buffer.BlockCopy(collisionMatrix, layerNumber * matrixLength, row, 0, matrixLength);
        collisionDictionary.Add(layerNumber, row);
      }

      CollisionMatrix = new ReadOnlyDictionary<int, bool[]>(collisionDictionary);
    }
  }
}