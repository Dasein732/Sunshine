using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Core.Surfaces
{
    public sealed class HitableList : Hitable
    {
        private readonly List<Hitable> list;

        public HitableList(List<Hitable> list)
        {
            this.list = list;
        }

        public override bool Hit(in Ray ray, float t_min, float t_max, ref HitRecord record)
        {
            HitRecord tempRecord = new HitRecord();
            var hitAnything = false;
            var closestSoFar = t_max;

            for(int i = 0; i < list.Count; i++)
            {
                if(list[i].Hit(ray, t_min, closestSoFar, ref tempRecord))
                {
                    hitAnything = true;
                    closestSoFar = tempRecord.T;
                    record = tempRecord;
                }
            }

            return hitAnything;
        }
    }
}