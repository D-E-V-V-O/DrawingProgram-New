using System;
using System.Runtime.Serialization;

namespace DrawingProgram {
    [Serializable]
    internal class OpenLoopException : Exception {
        public OpenLoopException() {
        }

        public OpenLoopException(string message) : base(message) {
        }

        public OpenLoopException(string message, Exception innerException) : base(message, innerException) {
        }

        protected OpenLoopException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}