using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingProgram {

    public class Codex {

        Block codex;
        List<String> stopwords = new List<String> { "loop", "end", "if", "endif" };
        Dictionary<String, int> vars = new Dictionary<string, int>();

        int count = 0;

        public Block GetBlock() {
            return codex;
        }
        public Codex(List<String> input) {
            List<String> preblock = new List<string>();
            preblock.AddRange(UnrollLoop(input));
            codex = new Block(preblock);
            
        }

        public List<String> ReadTo(List<String> commands, List<String> stops) {
            List<String> output = new List<String>();
            foreach (String command in commands) {
                if (!stops.Any(s => command.Contains(s))) {
                    output.Add(command);
                }
                count++;
            }
            return output;
        }

        public List<String> ReadTo(int index, List<String> commands, List<String> stops) {
            List<String> output = new List<String>();
            foreach (String command in commands) {
                if (commands.IndexOf(command) >= index) {
                    if (!stops.Any(s => command.Contains(s))) {
                        output.Add(command);
                    }
                    else return output;
                }
                count++;
            }
            return output;
        }

        public Block ReplaceVars(Block input) {
            String[] line;
            bool pass = true;
            List<String[]> lines = new List<string[]>();
            for (int i = 0; i < input.Length(); i++) {
                line = input.Step();
                if (line[0].Equals("var")) {
                    vars.Add(line[1], int.Parse(line[3]));
                    pass = false;
                }
                else {
                    foreach (KeyValuePair<String, int> entry in vars) {
                        if (line.Contains(entry.Key)) {
                            line[Array.IndexOf(line, entry.Key)] = entry.Value.ToString();
                        }
                    }
                }
                if (pass) lines.Add(line);
            }
            return new Block(lines);
        }

        public List<String> UnrollLoop(List<String> commands) {
            List<String> preblock = new List<String>();
            List<String> loopblock = new List<String>();
            bool add = false;
            int loop = 0;
            foreach(String command in commands) {
                if (command.Contains("loop")) {
                    add = true;
                    loop = int.Parse(command.Split(' ')[1]);
                }
                else if (command.Equals("end")) {
                    add = false;
                    for (int i = 0; i <= loop; i++) {
                        loopblock.AddRange(loopblock);
                        preblock.AddRange(loopblock);
                    }
                }
                else if (add) loopblock.Add(command);
                else preblock.Add(command);
            }
            
            return preblock;
        }

    }
}

