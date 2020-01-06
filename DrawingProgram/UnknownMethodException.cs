using System;
using System.Runtime.Serialization;

namespace DrawingProgram {
    [Serializable]
    internal class UnknownMthodException : Exception {
        public UnknownMthodException() {
        }

        public UnknownMthodException(string message) : base(message) {
        }

        public UnknownMthodException(string message, Exception innerException) : base(message, innerException) {
        }

        protected UnknownMthodException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}