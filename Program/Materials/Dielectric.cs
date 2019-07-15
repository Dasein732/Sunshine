using System;
using Microsoft.Xna.Framework;
using Sunshine.Utility;

namespace Program.Materials
{
    public sealed class Dielectric : Material
    {
        private readonly float _refIndex;
        private Vector3 _attenuation = new Vector3(1.0f, 1.0f, 1.0f);

        public Dielectric(float refIndex)
        {
            _refIndex = refIndex;
        }

        public override bool Scatter(in Ray ray, in HitRecord record, ref Vector3 Attenuation, ref Ray scattered)
        {
            Vector3 outwardNormal;
            Vector3 reflected = Vector3.Reflect(ray.Direction, record.Normal);
            float niOverNt;
            Vector3 refracted = new Vector3();
            _attenuation.CopyTo(ref Attenuation);
            float reflectProbe;
            float cosine;

            if(Vector3.Dot(ray.Direction, record.Normal) > 0)
            {
                outwardNormal = -record.Normal;
                niOverNt = _refIndex;
                cosine = _refIndex * Vector3.Dot(ray.Direction, record.Normal) / ray.Direction.Length();
            }
            else
            {
                outwardNormal = record.Normal;
                niOverNt = 1.0f / _refIndex;
                cosine = -Vector3.Dot(ray.Direction, record.Normal) / ray.Direction.Length();
            }

            if(Refract(ray.Direction, ref outwardNormal, niOverNt, ref refracted))
            {
                reflectProbe = Schlick(cosine, _refIndex);
            }
            else
            {
                reflectProbe = 1.0f;
            }

            if(XorShiftRandom.NextFloat() < reflectProbe)
            {
                scattered = new Ray(record.P, reflected);
            }
            else
            {
                scattered = new Ray(record.P, refracted);
            }

            return true;
        }

        private bool Refract(Vector3 vector, ref Vector3 normal, float niOverNt, ref Vector3 refracted)
        {
            Vector3 uv = Vector3.Normalize(vector);
            float dt = Vector3.Dot(uv, normal);
            float discriminant = 1.0f - niOverNt * niOverNt * (1 - dt * dt);

            if(discriminant > 0)
            {
                refracted = niOverNt * (uv - normal * dt) - normal * MathUtil.FastSqrt(discriminant);
                return true;
            }
            else
            {
                return false;
            }
        }

        private float Schlick(float cosine, float refIndex)
        {
            float r0 = (1 - refIndex) / (1 + refIndex);
            r0 *= r0;
            return (float)(r0 + (1 - r0) * (Math.Pow(1 - cosine, 5)));
        }
    }
}