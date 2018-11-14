// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;

namespace NeoSharp.Application
{
    /// <summary>
    /// Entry point of the NeoSharp-Light.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">arguments passes in the CLI.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1801", Justification="Main args")]
        public static void Main(string[] args)
        {
            var bootstraper = new Bootstrap();
            bootstraper.Start();

            Console.ReadLine();
        }
    }
}
