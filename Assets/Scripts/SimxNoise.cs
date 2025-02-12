﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimxNoise
{

    public class SimpleSimplex 
    {
        public class Grad
        {
            public double x, y, z, w;

            public Grad(double _x, double _y, double _z)
            {
                x = _x;
                y = _y;
                z = _z;
            }

            public Grad(double _x, double _y, double _z, double _w)
            {
                x = _x;
                y = _y;
                z = _z;
                w = _w;
            }
        }

        Grad[] grad3 = {
        new Grad (1, 1, 0),
        new Grad (-1, 1, 0),
        new Grad (1, -1, 0),
        new Grad (-1, -1, 0),
        new Grad (1, 0, 1),
        new Grad (-1, 0, 1),
        new Grad (1, 0, -1),
        new Grad (-1, 0, -1),
        new Grad (0, 1, 1),
        new Grad (0, -1, 1),
        new Grad (0, 1, -1),
        new Grad (0, -1, -1)
        };

        // Pseudorandom seed, shuffle this to seed the algorithm
        // NO DUPLICATE VALUES
        short[] p = {180,160,137,91,90,15,
        131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
        190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
        88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
        77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
        102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
        135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
        5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
        223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
        129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
        251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
        49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
        138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,151};
        short[] perm = new short[512];
        short[] permMod12 = new short[512];

        // Generates perm and permMod12 from the psuedorandom array p
        void permGen()
        {
            for (int i = 0; i < 512; i++)
            {
                perm[i] = p[i & 255];
                permMod12[i] = (short)(perm[i] % 12);
            }
            UnityEngine.Debug.Log("DEBUG: Finished generating permuations");
        }

        // Dot product using the Grad class
        double gradDot(Grad g, double x, double y)
        {
            return g.x * x + g.y * y;
        }

        double gradDot(Grad g, double x, double y, double z)
        {
            return g.x * x + g.y * y + g.z * z;
        }

        // Generates 2D simplex noise
        public double noise2D(double x_in, double y_in)
        {
            double n0, n1, n2;
            double s = (x_in + y_in) * (.5 * (Mathf.Sqrt(3.0f) - 1.0));
            //println("s: " + s);
            int i = (int)Mathf.Floor((float)(x_in + s));
            int j = (int)Mathf.Floor((float)(y_in + s));
            //println("i: " + i + ", j: " + j);
            double t = (i + j) * ((3.0 - Mathf.Sqrt(3.0f)) / 6.0);
            double dx0 = i - t;
            double dy0 = j - t;
            double x0 = x_in - dx0;
            double y0 = y_in - dy0;
            int i1, j1;
            if (x0 > y0) { i1 = 1; j1 = 0; } else { i1 = 0; j1 = 1; }
            double x1 = x0 - i1 + ((3.0 - Mathf.Sqrt(3.0f)) / 6.0);
            double y1 = y0 - j1 + ((3.0 - Mathf.Sqrt(3.0f)) / 6.0);
            double x2 = x0 - 1.0 + 2.0 * ((3.0 - Mathf.Sqrt(3.0f)) / 6.0);
            double y2 = y0 - 1.0 + 2.0 * ((3.0 - Mathf.Sqrt(3.0f)) / 6.0);

            int ii = i & 255;
            int jj = j & 255;
            int gi0 = permMod12[ii + perm[jj]];
            int gi1 = permMod12[ii + i1 + perm[jj + j1]];
            int gi2 = permMod12[ii + 1 + perm[jj + 1]];
            //println("gi0: " + gi0 + ", gi1:" + gi1 + ", gi2:" + gi2);
            double t0 = 0.5 - x0 * x0 - y0 * y0;
            if (t0 < 0) n0 = 0.0;
            else
            {
                t0 *= t0;
                n0 = t0 * t0 * gradDot(grad3[gi0], x0, y0);
            }
            double t1 = 0.5 - x1 * x1 - y1 * y1;
            if (t1 < 0) n1 = 0.0;
            else
            {
                t1 *= t1;
                n1 = t1 * t1 * gradDot(grad3[gi1], x1, y1);
            }
            double t2 = 0.5 - x2 * x2 - y2 * y2;
            if (t2 < 0) n2 = 0.0;
            else
            {
                t2 *= t2;
                n2 = t2 * t2 * gradDot(grad3[gi2], x2, y2);
            }
            //println("t0: " + t0 + ", t1:" + t1 + ", t2:" + t2);
            //println("n0: " + n0 + ", n1:" + n1 + ", n2:" + n2);
            return 70.0 * (n0 + n1 + n2);
        }

        // Generates 3D simplex noise
        //public double noise3D(double x_in, double y_in, double z_in)
        //{
        //    double s = (x_in + y_in + z_in) * .33;
        //    int i = (int)Mathf.Floor((float)(x_in + s));
        //    int j = (int)Mathf.Floor((float)(y_in + s));
        //    int k = (int)Mathf.Floor((float)(z_in + s));
        //    double t = (i + j + k) * (1.0 / 6.0);
        //    double _x0 = i - t;
        //    double _y0 = j - t;
        //    double _z0 = k - t;
        //    double x0 = x_in - _x0;
        //    double y0 = y_in - _y0;
        //    double z0 = z_in - _z0;
        //    int i1, j1, k1;
        //    int i2, j2, k2;
        //    if (x0 >= y0)
        //    {
        //        if (y0 >= z0)
        //        {
        //            i1 = 1; j1 = 0; k1 = 0;
        //            i2 = 1; j2 = 1; k2 = 0;
        //        }
        //        else if (x0 >= z0)
        //        {
        //            i1 = 1; j1 = 0; k1 = 0;
        //            i2 = 1; j2 = 0; k2 = 1;
        //        }
        //        else
        //        {
        //            i1 = 0; j1 = 0; k1 = 1;
        //            i2 = 1; j2 = 0; k2 = 1;
        //        }
        //    }
        //    else
        //    {
        //        if (y0 < z0)
        //        {
        //            i1 = 0; j1 = 0; k1 = 1;
        //            i2 = 0; j2 = 1; k2 = 1;
        //        }
        //        else if (x0 < z0)
        //        {
        //            i1 = 0; j1 = 1; k1 = 0;
        //            i2 = 0; j2 = 1; k2 = 1;
        //        }
        //        else
        //        {
        //            i1 = 0; j1 = 1; k1 = 0;
        //            i2 = 1; j2 = 1; k2 = 0;
        //        }
        //    }

        //    double x1 = x0 - i1 + (1.0 / 6.0);
        //    double y1 = y0 - j1 + (1.0 / 6.0);
        //    double z1 = z0 - k1 + (1.0 / 6.0);

        //    double x2 = x0 - i2 + 2.0 * (1.0 / 6.0);
        //    double y2 = y0 - j2 + 2.0 * (1.0 / 6.0);
        //    double z2 = z0 - k2 + 2.0 * (1.0 / 6.0);

        //    double x3 = x0 - 1.0 + 3.0 * (1.0 / 6.0);
        //    double y3 = y0 - 1.0 + 3.0 * (1.0 / 6.0);
        //    double z3 = z0 - 1.0 + 3.0 * (1.0 / 6.0);

        //    int ii = i & 255;
        //    int jj = j & 255;
        //    int kk = k & 255;

        //    int gi0 = permMod12[ii + perm[jj + perm[kk]]];
        //    int gi1 = permMod12[ii + i1 + perm[jj + j1 + perm[kk + k1]]];
        //    int gi2 = permMod12[ii + i2 + perm[jj + j2 + perm[kk + k2]]];
        //    int gi3 = permMod12[ii + 1 + perm[jj + 1 + perm[kk + 1]]];

        //    double n0, n1, n2, n3;

        //    double t0 = .6 - x0 * x0 - y0 * y0 - z0 * z0;
        //    if (t0 < 0) n0 = 0.0;
        //    else
        //    {
        //        t0 *= t0;
        //        n0 = t0 * t0 * gradDot(grad3[gi0], x0, y0, z0);
        //    }
        //    double t1 = .6 - x1 * x1 - y1 * y1 - z1 * z1;
        //    if (t1 < 0) n1 = 0.0;
        //    else
        //    {
        //        t1 *= t1;
        //        n1 = t1 * t1 * gradDot(grad3[gi1], x1, y1, z1);
        //    }
        //    double t2 = .6 - x2 * x2 - y2 * y2 - z2 * z2;
        //    if (t2 < 0) n2 = 0.0;
        //    else
        //    {
        //        t2 *= t2;
        //        n2 = t2 * t2 * gradDot(grad3[gi2], x2, y2, z2);
        //    }
        //    double t3 = .6 - x3 * x3 - y3 * y3 - z3 * z3;
        //    if (t3 < 0) n3 = 0.0;
        //    else
        //    {
        //        t3 *= t3;
        //        n3 = t3 * t3 * gradDot(grad3[gi3], x3, y3, z3);
        //    }
        //    return 32.0 * (n0 + n1 + n2 + n3);
        //}
    }
}