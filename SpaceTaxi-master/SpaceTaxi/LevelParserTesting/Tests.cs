using System;
using System.Collections.Generic;
using NUnit.Framework;
using SpaceTaxi_1.LevelParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SpaceTaxi_1.LevelParser;

namespace LevelParserTesting {
    [TestFixture]
    public class Tests {
        private ReadFile readShortNSweet;
        private ReadFile readTheBeach;
        private ReadFile readError;
        private List<string> shortList;
        private List<string> beachList;
        private Dictionary<char, string> shortnsweetDict;
        private Dictionary<char, string> thebeachDict;

        [SetUp]
        public void SetUp() {
            readTheBeach = new ReadFile();
            readTheBeach.Read("the-beach.txt");
            readShortNSweet = new ReadFile();
            readShortNSweet.Read("short-n-sweet.txt");
            readError = new ReadFile();
            shortList = new List<string> {
                "%#%#%#%#%#%#%#%#%#^^^^^^%#%#%#%#%#%#%#%#",
                "#               JW      JW             %",
                "%      h2g                             #",
                "#      222                     >       %",
                "%      H2G                        o    #",
                "#       3                           o  %",
                "%       3                              #",
                "#       3                           o  %",
                "%       3                       j%i    #",
                "#       3                       W Xi   %",
                "%       3                          %   #",
                "#                                 xI   %",
                "%    o                           xI    #",
                "#                               xI     %",
                "%                              xI      #",
                "#  o   o                      xI       %",
                "%                            xI        #",
                "#    o                      xI         %",
                "%      o                   xI          #",
                "#  o                      xI           %",
                "%       o                 I            #",
                "#         m1111111111111n              %",
                "%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#"
            };
            shortList.Reverse();
            beachList = new List<string> {
                "CTTTTTTTTTTTTTTTTD^^^^^^CTTTTTTTTTTTTttt",
                "A                                    stt",
                "A                                      B",
                "A HJJJJJJJJG                           B",
                "HIIIIIIIIIG                            B",
                "A  HIIIG                               B",
                "A                     prrrrrq          B",
                "A                      poooooqbc       B",
                "A                       poooooad       B",
                "A                        poooooq       B",
                "A                         poooooq      B",
                "A                          pooooo      B",
                "A                           aoooo      B",
                "A                           apooo      B",
                "A              >            a poo      B",
                "A                           a  po      B",
                "A ujl                       a   p      B",
                "A  ujl                      a          B",
                "A   ujl                     a          B",
                "A    ujiiiiiiiiiiiiiij      a          B",
                "A     gef         gef       a          B",
                "MMMMMMMeMMMMMMMMMMMeMMMMMMMMaMMMMMMMMMMM",
                "OOOOSSSeSSSSSSSSSSSeSSSSSSSOaOOOOOOOOOOO"
            };
            beachList.Reverse();
            shortnsweetDict = new Dictionary<char, string> {
                {'%', "white-square.png"},
                {'#', "ironstone-square.png"},
                {'1', "neptune-square.png"},
                {'2', "green-square.png"},
                {'3', "yellow-stick.png"},
                {'o', "purple-circle.png"},
                {'G', "green-upper-left.png"},
                {'H', "green-upper-right.png"},
                {'g', "green-lower-left.png"},
                {'h', "green-lower-right.png"},
                {'I', "ironstone-upper-left.png"},
                {'J', "ironstone-upper-right.png"},
                {'i', "ironstone-lower-left.png"},
                {'j', "ironstone-lower-right.png"},
                {'N', "neptune-upper-left.png"},
                {'M', "neptune-upper-right.png"},
                {'n', "neptune-lower-left.png"},
                {'m', "neptune-lower-right.png"},
                {'W', "white-upper-left.png"},
                {'X', "white-upper-right.png"},
                {'w', "white-lower-left.png"},
                {'x', "white-lower-right.png"}
            };
            thebeachDict = new Dictionary<char, string>() {
                {'A', "aspargus-edge-left.png"},
                {'B', "aspargus-edge-right.png"},
                {'T', "aspargus-edge-top.png"},
                {'C', "aspargus-edge-top-left.png"},
                {'D', "aspargus-edge-top-right.png"},
                {'G', "white-left-half-circle.png"},
                {'H', "white-right-half-circle.png"},
                {'I', "white-square.png"},
                {'J', "white-square.png"},
                {'O', "olive-green-square.png"},
                {'S', "salt-box-square.png"},
                {'M', "minsk-square.png"},
                {'a', "emperor-square.png"},
                {'b', "emperor-lower-right.png"},
                {'c', "emperor-lower-left.png"},
                {'d', "emperor-upper-left.png"},
                {'e', "deep-bronze-square.png"},
                {'f', "deep-bronze-left-half-circle.png"},
                {'g', "deep-bronze-right-half-circle.png"},
                {'i', "ironstone-square.png"},
                {'j', "ironstone-square.png"},
                {'l', "ironstone-lower-left.png"},
                {'u', "ironstone-upper-right.png"},
                {'o', "studio-square.png"},
                {'p', "studio-upper-right.png"},
                {'q', "studio-lower-left.png"},
                {'r', "studio-square.png"},
                {'t', "tacha-square.png"},
                {'s', "tacha-upper-right.png"}
            };
        }
        
        /// <summary>
        /// Tests if the board of short-n-sweet.txt is read as expected
        /// </summary>
        [Test]
        public void ShortNSweetBoardTest() {
            for (int i = 0; i < shortList.Count; i++) {
                Assert.True(String.Equals(shortList[i], readShortNSweet.BoardList[i]));   
            }            
        }

        /// <summary>
        /// Tests if the board of the-beach.txt is read as expected
        /// </summary>
        [Test]
        public void TheBeachBoardTest() {
            for (int i = 0; i < beachList.Count; i++) {
                Assert.True(String.Equals(beachList[i],readTheBeach.BoardList[i]));
            }
        }

        /// <summary>
        /// Tests if the dictionary of short-n-sweet.txt contains the right characters and strings in the right order
        /// </summary>
        [Test]
        public void ShortNSweetDictionaryTest() {
            for (int i = 0; i < shortnsweetDict.Count; i++) {
                Assert.AreEqual(shortnsweetDict.ElementAt(i), readShortNSweet.Dict.ElementAt(i));
            }
        }

        /// <summary>
        /// Tests if the dictionary of the-beach.txt contains the right characters and strings in the right order
        /// </summary>
        [Test]
        public void TheBeachDictionaryTest() {
            for (int i = 0; i < thebeachDict.Count; i++) {
                Assert.AreEqual(thebeachDict.ElementAt(i), readTheBeach.Dict.ElementAt(i));
            }
        }
        
        /// <summary>
        /// Tests if ReadFile.Read() throws an FileNotFoundException when given an invalid input
        /// </summary>
        [Test]
        public void TestReadFileReadException() {          
            DirectoryInfo dir = new DirectoryInfo(
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
            while (dir.Name != "bin") {
                dir = dir.Parent;
            }

            dir = dir.Parent;

            string path = Path.Combine(dir.FullName.ToString(), "Levels", "error.txt");
            
            var ex = Assert.Throws<FileNotFoundException>(() => readError.Read("error.txt"));
            Assert.That(ex.Message, Is.EqualTo($"Error: The file \"{path}\" does not exist."));
        }
    }
}