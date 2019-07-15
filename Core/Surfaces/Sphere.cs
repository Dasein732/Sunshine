using System;
using Core.Materials;
using Microsoft.Xna.Framework;

namespace Core.Surfaces
{
    public sealed class Sphere : Hitable
    {
        public Vector3 Center { get; set; }
        public float Radius { get; set; }

        public Material Material { get; }

        public Sphere()
        {
            Center = new Vector3();
            Radius = 1.0f;
        }

        public Sphere(Vector3 center, float radius, Material mat)
        {
            Center = center;
            Radius = radius;
            Material = mat;
        }

        public override bool Hit(in Ray ray, float t_min, float t_max, ref HitRecord record)
        {
            Vector3 oc = ray.Origin - Center;
            float a = Vector3.Dot(ray.Direction, ray.Direction);
            float b = Vector3.Dot(oc, ray.Direction);
            float c = Vector3.Dot(oc, oc) - Radius * Radius;
            float discriminant = b * b - a * c;

            if(discriminant > 0)
            {
                float temp = (-b - (float)Math.Sqrt(b * b - a * c)) / a;
                if(temp < t_max && temp > t_min)
                {
                    record.T = temp;
                    record.P = ray.PointAtParameter(record.T);
                    record.Normal = (record.P - Center) / Radius;
                    record.Material = Material;
                    return true;
                }

                temp = (-b + (float)Math.Sqrt(b * b - a * c)) / a;
                if(temp < t_max && temp > t_min)
                {
                    record.T = temp;
                    record.P = ray.PointAtParameter(record.T);
                    record.Normal = (record.P - Center) / Radius;
                    record.Material = Material;
                    return true;
                }
            }

            return false;
        }
    }
}