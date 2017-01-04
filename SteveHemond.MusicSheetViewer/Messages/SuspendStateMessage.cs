using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace SteveHemond.MusicSheetViewer.Messages
{
    public class SuspendStateMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuspendStateMessage"/> class.
        /// </summary>
        /// <param name="operation">The operation.</param>
        public SuspendStateMessage(SuspendingOperation operation)
        {
            this.Operation = operation;
        }

        /// <summary>
        /// Gets the operation.
        /// </summary>
        /// <value>The operation.</value>
        public SuspendingOperation Operation { get; }
    }
}
