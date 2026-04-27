using System;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid.Data
{
    public readonly struct HexCubeCoord : IEquatable<HexCubeCoord>
    {
        public int Q { get; }
        public int R { get; }
        public int S { get; }

        public HexCubeCoord(int q, int r)
        {
            Q = q;
            R = r;
            S = -q - r;
        }

        public static int Distance(HexCubeCoord a, HexCubeCoord b)
            => (Mathf.Abs(a.Q - b.Q) + Mathf.Abs(a.R - b.R) + Mathf.Abs(a.S - b.S)) / 2;

        // Flat-top: even-q offset (even columns shifted up, OFFSET_EVEN=-1 per redblobgames).
        // Pointy-top: even-r offset (even rows shifted right, OFFSET_EVEN=-1 per redblobgames).
        public static HexCubeCoord FromOffset(Vector2Int offset, bool isFlatTopped)
        {
            int col = offset.x;
            int row = offset.y;

            if (isFlatTopped)
            {
                int q = col;
                int r = row - (col - (col & 1)) / 2;
                return new HexCubeCoord(q, r);
            }
            else
            {
                int q = col - (row - (row & 1)) / 2;
                int r = row;
                return new HexCubeCoord(q, r);
            }
        }

        public override string ToString() => $"{Q} {R} {S}";

        public bool Equals(HexCubeCoord other) => Q == other.Q && R == other.R;
        public override bool Equals(object obj) => obj is HexCubeCoord c && Equals(c);
        public override int GetHashCode() => HashCode.Combine(Q, R);
        public static bool operator ==(HexCubeCoord a, HexCubeCoord b) => a.Equals(b);
        public static bool operator !=(HexCubeCoord a, HexCubeCoord b) => !a.Equals(b);
    }
}
