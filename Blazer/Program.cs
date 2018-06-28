/* 
 * Copyright (C) 2018 Kristofer Dovstam - All Rights Reserved
 */

using System;

namespace TheWarSpace
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Wars wars = new Wars())
                wars.Run();
        }
    }
}
