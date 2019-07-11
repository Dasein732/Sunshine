using Microsoft.Xna.Framework;

namespace Program.Surfaces
{
    public abstract class Material
    {
        public abstract bool Scatter(in Ray ray, in HitRecord record, Vector3 Attenuation, Ray scattered);
    }
}