using Core.Materials;
using Microsoft.Xna.Framework;

namespace Core.Surfaces
{
    public struct HitRecord
    {
        public float T;
        public Vector3 Normal;
        public Vector3 P;
        public Material Material;
    }
}