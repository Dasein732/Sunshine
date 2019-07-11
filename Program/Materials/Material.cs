using Microsoft.Xna.Framework;

namespace Program.Materials
{
    public abstract class Material
    {
        public abstract bool Scatter(in Ray ray, in HitRecord record, ref Vector3 Attenuation, ref Ray scattered);
    }
}