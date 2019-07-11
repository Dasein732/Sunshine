using System;
using Microsoft.Xna.Framework;
using Program.Util;

namespace Program.Materials
{
    public sealed class Metal : Material
    {
        private readonly Vector3 _albeido;
        private readonly float _fuzz;

        public Metal(Vector3 albeido, float fuzz)
        {
            _albeido = albeido;
            _fuzz = fuzz < 1 ? fuzz : 1;
        }

        public override bool Scatter(in Ray ray, in HitRecord record, ref Vector3 Attenuation, ref Ray scattered)
        {
            scattered.Origin = record.P;
            scattered.Direction = Reflect(Vector3.Normalize(ray.Direction), record.Normal) + _fuzz * RayTracer.RandomInUnitSphere();
            _albeido.CopyTo(ref Attenuation);

            return Vector3.Dot(scattered.Direction, record.Normal) > 0;
        }

        private Vector3 Reflect(in Vector3 vector, in Vector3 normal)
        {
            return vector - 2 * Vector3.Dot(vector, normal) * normal;
        }
    }
}