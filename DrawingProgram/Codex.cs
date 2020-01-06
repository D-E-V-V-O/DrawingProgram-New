using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace DrawingProgram {

    public class Codex : IEnumerable<String[]> {

        Block codex;
        List<String> stopwords = new List<String> { "loop", "end", "if", "endif" };
        Dictionary<String, int> vars = new Dictionary<string, int>();
        Dictionary<String, Method> methods = new Dictionary<String, Method>();
        
        public Codex(List<String> input) {
            codex = new Block(input);
            Transpile();
        }

        public Codex(List<String[]> input){
            codex = new Block(input);
            Transpile();
        }

        private void Transpile(){
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
            List<String[]> preblock = new List<String[]>();
            List<String[]> loopblock = new List<String[]>();
            bool add = false; 
            int loop = 0;
            foreach(String[] command in masterblock) {
                if (command[0].Equals("loop")) {
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
                else if (add) loopblock.Add(command);
                else preblock.Add(command);
            }
            return new Block(preblock);
        }

        private Block EvaluateIf(Block masterblock){
            List<String[]> preblock = new List<String[]>();
            bool add, invert, ifblock;
            int left, right;
            String[] condition = new String[4];
            condition[1] = "$";
            ifblock = false;
            invert = false;
            bool include = false;
            foreach(String[] command in masterblock){
                add = false;
                if(command[0].Equals("if")){
                    include = false;
                    left = int.Parse(command[1]);
                    right = int.Parse(command[3]);
                    if(command[2].StartsWith("!")){
                            invert = true;
                            command[2] = command[2].Substring(1);
                        }                    
                    switch(command[2]){
                            case "==":
                                if (left == right) include = true;
                                break;
                            case ">":
                                if (left > right) include = true;
                                break;
                            case "<":
                                if (left < right) include = true;
                                break;
                            case ">=":
                                if (left >= right) include = true;
                                break;
                            case "<=":
                                if (left <= right) include = true;
                                break;
                        }
                    if (command[command.Length - 1].Equals(":")){
                        ifblock = true;
                        add = false;
                    }
                    else {  
                        int j = Array.IndexOf(command, ":");
                        int k = 0;
                        condition = new string[command.Length - j - 1];
                        for(int i = 0; i < command.Length; i++){
                            if (i >= j + 1){
                                condition[k] = command[i];
                                k++;
                            }
                        }
                        add = true;
                        
                    }
                    bool tmp;
                    tmp = invert ? !include : include;
                    if(add && tmp){                        
                        preblock.Add(condition);
                    }
                }
                else if (command[0].Equals("endif")){
                    ifblock = false;
                }
                else{
                    bool tmp;
                    tmp = invert ? !include : include;
                    if(ifblock && tmp) preblock.Add(command);
                    else if (!ifblock) preblock.Add(command);
                }
            }
            return new Block(preblock);
        }

        private Block ExpandMethods(Block masterblock){
            bool add = false;
            List<String[]> preblock = new List<string[]>();
            List<String[]> tempblock = new List<string[]>();
            String name = null;
            String[] temparr;
            Method method;
            foreach(String[] command in masterblock){                
                if(command[0].Equals("method")){
                    add = true;
                    name = command[1];
                    tempblock = new List<string[]>();
                    tempblock.Add(command);
                }                
                else if (command[0].Equals("endmethod")){
                    add = false;
                    methods.Add(name, new Method(tempblock));
                }
                else if (add == true) {
                    tempblock.Add(command);
                }
                else if (command[0].Equals("call")){
                    if (methods.TryGetValue(command[1], out method)) {
                        tempblock = method.GetFullBlock();
                        for (int i = 0; i < command.Length; i++) {
                            if (method.HasParams() && method.GetDict().ContainsKey(command[i])) {
                                method.SetValue(method.paramnames[i - 2], int.Parse(command[i]));
                            }
                        }
                        foreach (String[] words in tempblock) {
                            temparr = new String[words.Length];
                            for(int i = 0; i < words.Length; i++) {
                                if (method.HasParams() && method.GetDict().ContainsKey(words[i])){
                                    if (method.GetDict().ContainsKey(words[i])) {
                                        temparr[i] = method.GetDict()[words[i]].ToString();
                                    }
                                    else temparr[i] = words[i];
                                }
                            }
                            if (!words[0].Equals("call")) preblock.Add(temparr);
                        }
                    }
                }
                else preblock.Add(command);
            }
            return new Block(preblock);
        }


        // BEGIN INTERFACE IMPLEMENTATION
        public IEnumerable<String[]> Enum(){
            foreach (String[] line in codex){
                yield return line;
            }
        }

        public IEnumerator<String[]> GetEnumerator() {
            return codex.GetEnumerator();
        }

        IEnumerator<String[]> IEnumerable<String[]>.GetEnumerator() {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
        // END INTERFACE IMPLEMENTATION
    }
}

