using System;
using UnityEngine;

namespace Forma.Runtime.HexGrid.Data
{
    public readonly struct HexCubeCoordinates : IEquatable<HexCubeCoordinates>
    {
        public int Q { get; }
        public int R { get; }
        public int S { get; }

        public HexCubeCoordinates(int q, int r)
        {
            Q = q;
            R = r;
            S = -q - r;
        }

        public HexCubeCoordinates[] GetNeighbours()
        {
            return new[]
            {
                new HexCubeCoordinates(Q + 1, R - 1),
                new HexCubeCoordinates(Q + 1, R),
                new HexCubeCoordinates(Q, R + 1),
                new HexCubeCoordinates(Q - 1, R + 1),
                new HexCubeCoordinates(Q - 1, R),
                new HexCubeCoordinates(Q, R - 1)
            };
        }

        public static int Distance(HexCubeCoordinates a, HexCubeCoordinates b)
            => (Mathf.Abs(a.Q - b.Q) + Mathf.Abs(a.R - b.R) + Mathf.Abs(a.S - b.S)) / 2;

        public bool Equals(HexCubeCoordinates other)
            => Q == other.Q && R == other.R;

        public override string ToString()
            => $"{Q} {R} {S}";

        public override bool Equals(object obj)
            => obj is HexCubeCoordinates c && Equals(c);

        public override int GetHashCode()
            => HashCode.Combine(Q, R);

        public static bool operator ==(HexCubeCoordinates a, HexCubeCoordinates b)
            => a.Equals(b);

        public static bool operator !=(HexCubeCoordinates a, HexCubeCoordinates b)
            => !a.Equals(b);
    }
}
