using System;
using Microsoft.Xna.Framework;
using Program.Util;

namespace Program.Materials
{
    public sealed class Lambertian : Material
    {
        private readonly Vector3 Albeido;

        public Lambertian(Vector3 albeido)
        {
            Albeido = albeido;
        }

        public override bool Scatter(in Ray ray, in HitRecord record, ref Vector3 Attenuation, ref Ray scattered)
        {
            var target = record.P + record.Normal + RayTracer.RandomInUnitSphere();
            scattered.Origin = record.P;
            scattered.Direction = target - record.P;
            Albeido.CopyTo(ref Attenuation);

            return true;
        }
    }
}