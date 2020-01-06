using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingProgram {
    class Method : Block{
        Dictionary<String, int?> parameters;
        public List<String> paramnames;
        bool hasparams = false;
        public Method(List<String[]> input)  {
            String[] header = input[0];
            int count = 0;
            foreach (String[] command in input) {
                if (count == 0) {
                    for (int i = 0; i < header.Length; i++) {
                        if (i > 1) {
                            SetParameter(header[i], null);
                            hasparams = true;
                            paramnames.Add(header[i]);
                        }
                    }
                }
                else lines.Add(command);
                count++;
            }
        }

        public bool HasParams() {
            return hasparams;
        }

        public void SetParameter(String key, int? value) {
            parameters.Add(key, value);
        }

        public void SetValue(String key, int? value) {
            parameters[key] = value;
        }

        public int? GetParameterValue(String key) {
            return parameters[key];
        }

        public Dictionary<String, int?> GetDict() {
            return parameters;
        }
    }
}
