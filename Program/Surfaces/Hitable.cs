namespace Program
{
    public abstract class Hitable
    {
        public abstract bool Hit(in Ray ray, float t_min, float t_max, ref HitRecord record);
    }
}