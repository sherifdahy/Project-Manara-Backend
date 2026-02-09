using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Consts;

public static class DefaultScopes
{
    public static class System
    {
        public const int Id = 1;
        public const string Name = "System";
    }

    public static class University
    {
        public const int Id = 2;
        public const string Name = "University";
    }

    public static class Faculty
    {
        public const int Id = 3;
        public const string Name = "Faculty";
    }

    public static class Department
    {
        public const int Id = 4;
        public const string Name = "Department";
    }

    public static class Program
    {
        public const int Id = 5;
        public const string Name = "Program";
    }
}

