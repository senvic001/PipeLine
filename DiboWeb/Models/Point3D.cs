namespace DiboWeb.Models
{
    public struct Point3D
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public Point3D(double x,double y,double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (this.GetType() != obj.GetType()) return false;

            Point3D point = (Point3D)obj;
            return (this.X == point.X && this.Y == point.Y && this.Z == point.Z);
        }
        public static bool Equals(Point3D point1,Point3D point2)
        {
            return (point1.X == point2.X && point1.Y == point2.Y && point1.Z == point2.Z);
        }

        public static bool Equals(Point3D point1, double x,double y,double z)
        {
            return (point1.X ==x && point1.Y ==y && point1.Z== z);
        }
        public static bool Equals2D(Point3D point1, double x, double y)
        {
            return (point1.X == x && point1.Y == y );
        }
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
        public override string ToString()
        {
            return string.Format("{0},{1},{2}", X, Y, Z);
        }
    }
}
