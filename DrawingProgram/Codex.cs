using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingProgram {

	public class Codex {

		List<String> codex = new List<String>();
		List<String> stopwords = new List<String> { "loop", "end", "if", "endif" };

		int count = 0;

		public List<String> GetCodex() {
			return codex;
		}
		public Codex(List<String> input) {
			// if(input[0].Contains("Method") { }
			codex.AddRange(ReadTo(count, input, stopwords));
			String[] stopline = input[count].Split(' ');
			switch (stopline[0]) {
				case "loop":
					codex.AddRange(UnrollLoop(count, input, int.Parse(stopline[1])));
					break;
				case "end":
					MessageBox.Show("end");
					break;
				case "if":
					MessageBox.Show("if");
					break;
				case "endif":
					MessageBox.Show("endif");
					break;
			}
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

		public List<String> UnrollLoop(int index, List<String> commands, int loop) {
			List<String> output = new List<String>();
			bool thrower = true;
			for (int i = 0; i < loop; i++) {
				foreach (String command in commands) {
					if (commands.IndexOf(command) >= index) {
						if (!command.Equals("end")) {
							output.Add(command);
						}
						else {
							thrower = false;
							break;
						}
					}
				}
			}
			if (thrower == true) throw new OpenLoopException();
			else return output;
		}

	}
}
