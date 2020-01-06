using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DrawingProgram {
    public class Block : IEnumerable<String[]> {

        protected List<String[]> lines;
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
            pos = 0;
        }

        public Block() {
            lines = new List<String[]>();
            pos = 0;
        }

        public String[] Step() {
            if (pos  < lines.Count()) {
                pos++;
                return lines[pos - 1];
            }
            else return lines[lines.Count - 1];
        }

        public String[] StepBack() {
            if (pos > 0) {
                pos--;
                return lines[pos + 1];
            }
            else return lines[0];
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

        public IEnumerator<String[]> GetEnumerator() {
            return lines.GetEnumerator();
        }

        IEnumerator<String[]> IEnumerable<String[]>.GetEnumerator() {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

    }
}
