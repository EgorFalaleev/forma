using System;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    [Serializable]
    public struct HexCoordinates
    {
        [SerializeField]
        int _x,
            _z;

        public int x => _x;
        public int z => _z;
        public int y => -x - z;

        public HexCoordinates(int x, int z)
        {
            _x = x;
            _z = z;
        }

        public static HexCoordinates FromOffsetCoordinates(int x, int z)
        {
            return new HexCoordinates(x - z / 2, z);
        }

        public override string ToString()
        {
            return $"({x.ToString()}, {y.ToString()}, {z.ToString()})";
        }

        public string ToStringOnSeparateLines()
        {
            return $"{x.ToString()}\n{y.ToString()}\n{z.ToString()}";
        }
    }
}