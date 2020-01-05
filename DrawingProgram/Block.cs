using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingProgram {
    public class Block {

        List<String[]> lines;
        int pos;

        public Block(List<String[]> input) {
            lines = new List<String[]>();
            lines = input;
            pos = 0;
        }

        public Block(List<String> input) {
            lines = new List<String[]>();
            foreach (String line in input) {
                String[] tokens = line.Split(' ');
                lines.Add(tokens);
            }
        }

        public String[] Step() {
            if (pos  < lines.Count()) {
                pos++;
                return lines[pos - 1];
            }
            else return null;
        }

        public IEnumerable<String[]> Enum() {
            foreach (String[] line in lines) {
                yield return line;
            }
        }

        public int Length() {
            return lines.Count();
        }

        public List<String[]> GetFullBlock() {
            return lines;
        }

        public void ResetPos() {
            pos = 0;
        }

        public void AppendBlock(List<String[]> annex) {
            lines.AddRange(annex);
        }

        public void AppendBlock(Block annex) {
            lines.AddRange(annex.GetFullBlock());
        }

        public void AppendBlock(List<String> annex) {
            List<String[]> preblock = new List<string[]>();
            foreach (String line in annex) {
                String[] tokens = line.Split(' ');
                preblock.Add(tokens);
            }
            lines.AddRange(preblock);
        }

    }
}
