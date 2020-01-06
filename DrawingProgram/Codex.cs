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
        
        public Codex(List<String> input) {
            codex = new Block();
            codex = ExpandMethods(codex);
            codex = ReplaceVars(codex);
            codex = EvaluateIf(codex);
            codex = UnrollLoop(codex);            
        }

        public Block GetBlock() {
            return codex;
        }

        private Block ReplaceVars(Block masterblock) {
            bool pass;
            List<String[]> lines = new List<string[]>();
            foreach (String[] line in masterblock) {
                pass = true;
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

        private Block UnrollLoop(Block masterblock) {
            List<String> preblock = new List<String>();
            List<String> loopblock = new List<String>();
            bool add = false; 
            int loop = 0;
            foreach(String[] command in masterblock) {
                if (command.Contains("loop")) {
                    add = true;
                    loop = int.Parse(command[1]); // Grab the loop number from the second word of the command
                }
                else if (command[0].Equals("end")) {
                    add = false;
                    for (int i = 1; i < loop; i++) {
                        loopblock.AddRange(loopblock);
                    }
                    preblock.AddRange(loopblock);
                }
                else if (add) loopblock.Add(command.ToString());
                else preblock.Add(command.ToString());
            }
            return new Block(preblock);
        }

        private Block EvaluateIf(Block masterblock){
            return masterblock;
        }

        private Block ExpandMethods(Block masterblock){
            return masterblock;
        }

        public IEnumerable<String[]> Enum(){
            foreach (String[] line in codex.Enum()){
                yield return line;
            }
        }

    }
}

