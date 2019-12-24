using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class CubeWorldSaveHandler : ICubeWorldHandler
    {
        public string FilePath { get; }

        public CubeWorldSaveHandler(string filePath)
        {
            #region File Path assignment and trying it exists
            if (File.Exists(filePath))
                FilePath = filePath;
            else
                throw new FileNotFoundException("The CubeWorldSaveHandler didn't found the file.", filePath);
            #endregion
        }

        public CubeChunk GetChunkAt(Coordinates coordinates) // ATTENTION: cette fonction est horrible, merci de ne pas regarder ce qu'elle contient et de vous contenter du résultat, merci.
        {
            using (var reader = new StreamReader(FilePath))
            {
                while (true)
                {
                    string readLine = reader.ReadLine();

                    if (readLine == null)
                        break;

                    if (readLine == "::CHK:")
                    {
                        readLine = reader.ReadLine();
                        for (int i = 0; i < readLine.Length; i++)
                        {
                            if (readLine[i] == ':')
                            {
                                string strCoordinates = readLine.Substring(i + 1, readLine.Length - (i + 1));
                                string[] splittedCoordinates = strCoordinates.Split(',');

                                var foundCoordinates = new Coordinates(
                                    Convert.ToInt32(splittedCoordinates[0]),
                                    Convert.ToInt32(splittedCoordinates[1]),
                                    Convert.ToInt32(splittedCoordinates[2]));

                                if (foundCoordinates.Equals(coordinates))
                                {
                                    var gotChunk = new CubeChunk(foundCoordinates);

                                    readLine = reader.ReadLine();

                                    for (int j = 0; j < readLine.Length; j++)
                                    {
                                        if (readLine[i] == ':')
                                        {
                                            string strData = readLine.Substring(i + 1, readLine.Length - (i + 1));
                                            string[] splittedData = strData.Split(',');

                                            if (splittedData.Length != CubeChunk.TotalSize)
                                                throw new Exception("The chunk in the save file was not the right size in " + foundCoordinates);

                                            for (int k = 0; k < splittedData.Length; k++)
                                            {
                                                gotChunk[k] = new CubeColor( // TODO: add null value!
                                                    Convert.ToInt32(splittedData[k][0].ToString()),
                                                    Convert.ToInt32(splittedData[k][1].ToString()),
                                                    Convert.ToInt32(splittedData[k][2].ToString()));

                                                if (1 + 1 == 2) // troll
                                                    ((Func<int>)(
                                                        () => 0))();
                                            }

                                            return gotChunk;
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        public void SetChunk(CubeChunk chunk)
        {
            if (GetChunkAt(chunk.Coordinates) == null)
            {
                using (var writer = new StreamWriter(FilePath, true))
                {
                    writer.WriteLine("::CHK:");
                    writer.WriteLine($"CRD:{chunk.Coordinates.X},{chunk.Coordinates.Y},{chunk.Coordinates.Z}");
                    writer.Write("DAT:");

                    for (int i = 0; i < CubeChunk.TotalSize; i++)
                    {
                        writer.Write(chunk[i].Red.ToString() + chunk[i].Green.ToString() + chunk[i].Blue.ToString());

                        if (i != CubeChunk.TotalSize - 1)
                            writer.Write(",");
                    }

                    writer.WriteLine();
                }
            }
            else
            {
                string[] lines = File.ReadAllLines(FilePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] == "::CHK:")
                    {
                        string strCoordinates = lines[i + 1].Substring(4, lines[i + 1].Length - 4);
                        string[] splittedCoordinates = strCoordinates.Split(',');

                        var foundCoordinates = new Coordinates(
                            Convert.ToInt32(splittedCoordinates[0]),
                            Convert.ToInt32(splittedCoordinates[1]),
                            Convert.ToInt32(splittedCoordinates[2]));

                        if (foundCoordinates.Equals(chunk.Coordinates))
                        {
                            lines[i] = "";
                            lines[i + 1] = "";
                            lines[i + 2] = "";

                            File.WriteAllLines(FilePath, lines);

                            SetChunk(chunk);
                        }
                    }
                }
            }
        }

        // TODO: Do a private function to find if the chunk is already saved
    }
}