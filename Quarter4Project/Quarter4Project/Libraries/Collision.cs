﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter4Project
{
    /// <summary>
    /// 
    /// </summary>
    public static class Collision
    {

        #region Structs

        /// <summary>
        /// Creates map segments
        /// </summary>
        /// <param name="mapSegment">Points to Vectors to Lines</param>
        public struct mapSegment
        {

            public mapSegment(Point a, Point b)
            {
                p1 = a;
                p2 = b;
            }
            public Point p1;
            public Point p2;

            public Vector2 getVector()
            {
                return new Vector2(p2.X - p1.X, p2.Y - p1.Y);
            }

            public Rectangle collisionRect()
            {
                return new Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
            }

        }

        /// <summary>
        /// Creates a line from point A to point B
        /// </summary>
        public struct line2D
        {
            public Vector2 P;
            public Vector2 V;

            public float yInt()
            {
                return (-V.Y * P.X + V.X * P.Y) / V.X;
            }

            public float Slope()
            {
                return V.Y / V.X;
            }

        }

        /// <summary>
        /// Creates a circle.
        /// </summary>
        public struct Circle
        {
            public Vector2 P;
            public double R;

            public Circle(Vector2 p, double r)
            {
                P = p;
                R = r;
            }
        }

        /// <summary>
        /// To be removed.
        /// </summary>
        public struct Ellipse
        {
            public Vector2 P;
            public double radX;
            public double radY;
            public double rotation;

            public Ellipse(Vector2 p, double x, double y, double r)
            {
                P = p;
                radX = x;
                radY = y;
                rotation = r;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Magnitude of a vector.
        /// </summary>
        /// <param name="v">Vector</param>
        /// <returns>float magnitude of vector.</returns>
        public static float magnitude(Vector2 v)
        {
            return (float)Math.Sqrt((v.X * v.X) + (v.Y * v.Y));
        }

        /// <summary>
        /// Unit vector of a vector.
        /// </summary>
        /// <param name="v">Vector</param>
        /// <returns>unit vector of vector</returns>
        public static Vector2 unitVector(Vector2 v)
        {
            return new Vector2((v.X / (float)magnitude(v)), (v.Y / (float)magnitude(v)));
        }

        /// <summary>
        /// Finds the normal of the vector
        /// </summary>
        /// <param name="v">Vector</param>
        /// <returns>Vector normal of vector v</returns>
        public static Vector2 vectorNormal(Vector2 v)
        {
            return new Vector2(-v.Y, v.X);
        }

        /// <summary>
        /// Find dot product of vector A and vector B
        /// </summary>
        /// <param name="u">Vector A</param>
        /// <param name="v">Vector B</param>
        /// <returns>Float value dot product of vector A and vector B</returns>
        public static float dotProduct(Vector2 u, Vector2 v)
        {
            return (u.X * v.X) + (u.Y * v.Y);
        }

        /// <summary>
        /// Finds reflection vector of Vector A and Vector B
        /// </summary>
        /// <param name="v">Vector A</param>
        /// <param name="a">Vector B</param>
        /// <returns>Vector2 reflection vector of vector A and vector B</returns>
        public static Vector2 reflectionVector(Vector2 v, Vector2 a)
        {
            Vector2 n = vectorNormal(a);
            float co = -2 * (dotProduct(v, n) / (magnitude(n) * magnitude(n)));
            Vector2 r;
            r.X = v.X + co * n.X;
            r.Y = v.Y + co * n.Y;
            return r;
        }

        /// <summary>
        /// Checks the collision between a map segment and a circle.
        /// </summary>
        /// <param name="C">Circle</param>
        /// <param name="S">Map Segment</param>
        /// <returns>True or False if the circle and mapsegment is colliding or not</returns>
        public static bool CheckCircleSegmentCollision(Circle C, mapSegment S)
        {
            line2D L;
            L.P.X = S.p1.X;
            L.P.Y = S.p1.Y;
            L.V.X = S.p2.X - S.p1.X;
            L.V.Y = S.p2.Y - S.p1.Y;

            double OH = Math.Abs(((L.V.X * (C.P.Y - L.P.Y)) - (L.V.Y * (C.P.X - L.P.X))) / (Math.Sqrt(L.V.X * L.V.X + L.V.Y * L.V.Y)));

            if (OH <= C.R)
            {
                Vector2 CollisionPoint1;
                Vector2 CollisionPoint2;
                if (L.V.X != 0)
                {
                    double Dv = L.V.Y / L.V.X;
                    double E = (L.V.X * L.P.Y - L.V.Y * L.P.X) / L.V.X - C.P.Y;

                    double a = 1 + Dv * Dv;
                    double b = -2 * C.P.X + 2 * E * Dv;
                    double c = C.P.X * C.P.X + E * E - C.R * C.R;

                    CollisionPoint1.X = (float)((-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a));
                    CollisionPoint2.X = (float)((-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a));
                    CollisionPoint1.Y = L.Slope() * CollisionPoint1.X + L.yInt();
                    CollisionPoint2.Y = L.Slope() * CollisionPoint2.X + L.yInt();

                    bool cond1 = (Math.Min(S.p1.X, S.p2.X) <= CollisionPoint1.X && CollisionPoint1.X <= Math.Max(S.p1.X, S.p2.X));
                    bool cond2 = (Math.Min(S.p1.Y, S.p2.Y) <= CollisionPoint1.Y && CollisionPoint1.Y <= Math.Max(S.p1.Y, S.p2.Y));
                    bool cond3 = (Math.Min(S.p1.X, S.p2.X) <= CollisionPoint2.X && CollisionPoint2.X <= Math.Max(S.p1.X, S.p2.X));
                    bool cond4 = (Math.Min(S.p1.Y, S.p2.Y) <= CollisionPoint2.Y && CollisionPoint2.Y <= Math.Max(S.p1.Y, S.p2.Y));

                    return (cond1 && cond2) || (cond3 && cond4);
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if a mapsegment is colliding with a mapsegment
        /// </summary>
        /// <param name="s1">mapsegment A</param>
        /// <param name="s2">mapsegment B</param>
        /// <returns>True or False if the mapsegments are colliding or not.</returns>
        public static bool CheckSegmentSegmentCollision(mapSegment s1, mapSegment s2)
        {
            line2D l1, l2;

            l1.P = new Vector2(s1.p1.X, s1.p1.Y);
            l2.P = new Vector2(s2.p1.X, s2.p1.Y);
            l1.V.X = s1.p2.X - s1.p1.X;
            l1.V.Y = s1.p2.Y - s1.p1.Y;
            l2.V.X = s2.p2.X - s2.p1.X;
            l2.V.Y = s2.p2.Y - s2.p1.Y;

            Vector2 collisionPoint;

            collisionPoint.X = (l2.yInt() - l1.yInt()) / (l1.Slope() - l2.Slope());
            collisionPoint.Y = l1.Slope() * collisionPoint.X + l1.yInt();

            bool cond1 = (Math.Min(s1.p1.X, s1.p2.X) <= collisionPoint.X && collisionPoint.X <= Math.Max(s1.p1.X, s1.p2.X));
            bool cond2 = (Math.Min(s2.p1.X, s2.p2.X) <= collisionPoint.X && collisionPoint.X <= Math.Max(s2.p1.X, s2.p2.X));
            bool cond3 = (Math.Min(s1.p1.Y, s1.p2.Y) <= collisionPoint.Y && collisionPoint.Y <= Math.Max(s1.p1.Y, s1.p2.Y));
            bool cond4 = (Math.Min(s2.p1.Y, s2.p2.Y) <= collisionPoint.Y && collisionPoint.Y <= Math.Max(s2.p1.Y, s2.p2.Y));


            return cond1 && cond2 && cond3 && cond4;
        }

        /// <summary>
        /// Checks collision between two circles.
        /// </summary>
        /// <param name="C1">Circle A</param>
        /// <param name="C2">Circle B</param>
        /// <returns>True or False if the circles are colliding or not.</returns>
        public static bool CheckCircleCircleCollision(Circle C1, Circle C2)
        {
            if (C1.R + C2.R >= magnitude(C2.P - C1.P))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks collision between a circle and a rectangle.
        /// </summary>
        /// <param name="C">Circle</param>
        /// <param name="R">Rectangle</param>
        /// <returns>True or False if the circle and rectangle are colliding or not.</returns>
        public static bool CheckCircleRectangleCollision(Circle C, Rectangle R)
        {
            Point[] points = new Point[] { new Point(R.X, R.Y), new Point(R.X + R.Width, R.Y), new Point(R.X, R.Y + R.Height), new Point(R.X + R.Width, R.Y + R.Height) };
            mapSegment[] segs = new mapSegment[] { new mapSegment(points[0], points[2]), new mapSegment(points[2], points[3]), new mapSegment(points[3], points[1]), new mapSegment(points[1], points[0]) };
            Boolean loopBreak = false;
            for (int i = 0; i < segs.Length && !loopBreak; i++)
            {
                loopBreak = CheckCircleSegmentCollision(C, segs[i]);
            }
            return loopBreak;
        }

        /// <summary>
        /// To be removed.
        /// </summary>
        /// <param name="E"></param>
        /// <param name="F"></param>
        /// <returns></returns>
        public static bool CheckEllipseEllipseCollision(Ellipse E, Ellipse F)
        {
            /* 
             * rotation = (float)Math.Atan(direction.Y / direction.X);
             * flip = Math.Sign(direction.X) == 1;
             */
            Vector2 v = new Vector2(F.P.X - E.P.X, F.P.Y - E.P.Y);
            double a = getAngleFromVector(v);
            double b = getAngleFromVector(new Vector2(-v.X, -v.Y));
            double Er, Fr;
            Er = Math.Sqrt(Math.Pow((Math.Cos(a + E.rotation) * E.radX), 2) + Math.Pow(Math.Cos(90 - a + E.rotation) * E.radY, 2));
            Fr = Math.Sqrt(Math.Pow((Math.Cos(b + F.rotation) * F.radX), 2) + Math.Pow(Math.Cos(90 - b + F.rotation) * F.radY, 2));
            double z = Er + Fr;
            double x = magnitude(v);
            if (Er + Fr >= magnitude(v))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double getAngleFromVector(Vector2 v)
        {
            return Math.Atan2(v.Y, v.X) * 180 / Math.PI;
        }

        #endregion

    }
}
