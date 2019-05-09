using System;
using System.Collections.Generic;
using System.IO;

namespace SpaceTaxi_1.LevelParser {
    public class ReadFile {
        private string line;
        private int counter;
        public Dictionary<char, string> Dict;
        public List<string> BoardList;
        
        /// <summary>
        /// Adds the level-map as strings to BoardList.
        /// Adds the char and .png-string to Dict as key and value.
        /// </summary>
        /// <param name="level">Filename of the level</param>
        public void Read(string level) {
            StreamReader file = new StreamReader(GetLevelPath(level));
            BoardList = new List<string>();
            Dict = new Dictionary<char, string>();           
            
            // Reads and adds the line to lists.            
            while ((line = file.ReadLine()) != null) {
                counter++;
                if (line.EndsWith(".png")) {
                    Dict.Add(line[0],line.Substring(3));
                }
                if (counter < 24) {
                    BoardList.Add(line);
                }
            }
            BoardList.Reverse();
            file.Close();
        }

        /// <summary>
        /// Finds full directory path of the given level.
        /// </summary>
        /// <remarks>This code is borrowed from Texture.cs in DIKUArcade.</remarks>
        /// <param name="filename">Filename of the level.</param>
        /// <returns>Directory path of the level.</returns>
        /// <exception cref="FileNotFoundException">File does not exist.</exception>
        private string GetLevelPath(string fileName) {
            DirectoryInfo dir = new DirectoryInfo(
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
            while (dir.Name != "bin") {
                dir = dir.Parent;
            }
            dir = dir.Parent;
            string path = Path.Combine(dir.FullName.ToString(), "Levels", fileName);
            if (!File.Exists(path)) {
                throw new FileNotFoundException($"Error: The file \"{path}\" does not exist.");
            }
            return path;
        }
    }
}