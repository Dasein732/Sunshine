using Microsoft.Xna.Framework;
using Program.Surfaces;

namespace Program
{
    public struct HitRecord
    {
        public float T;
        public Vector3 Normal;
        public Vector3 P;
        public Material material;
    }
}