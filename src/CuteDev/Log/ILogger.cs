using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev.Logger
{
    //
    // Summary:
    //     Describes a logging interface which is used for outputting messages.
    public interface ILogger
    {
        //
        // Summary:
        //     Gets an indication whether debug output is logged or not.
        bool IsDebugEnabled { get; }

        //
        // Summary:
        //     Logs a debug message.
        //
        // Parameters:
        //   message:
        //     The message to log.
        //
        //   formatArgs:
        //     String.Format arguments (if applicable).
        void Debug(string message, params object[] formatArgs);
        //
        // Summary:
        //     Logs an error message.
        //
        // Parameters:
        //   message:
        //     The message to log.
        //
        //   formatArgs:
        //     String.Format arguments (if applicable).
        void Error(string message, params object[] formatArgs);
        //
        // Summary:
        //     Logs an error message resulting from an exception.
        //
        // Parameters:
        //   exception:
        //
        //   message:
        //     The message to log.
        //
        //   formatArgs:
        //     String.Format arguments (if applicable).
        void Error(Exception exception, params object[] formatArgs);
        void Error(Exception exception, string message, params object[] formatArgs);
        //
        // Summary:
        //     Returns a logger which will be associated with the specified type.
        //
        // Parameters:
        //   type:
        //     Type to which this logger belongs.
        //
        // Returns:
        //     A type-associated logger.
        ILogger ForType(Type type);
        //
        // Summary:
        //     Returns a logger which will be associated with the specified type.
        //
        // Returns:
        //     A type-associated logger.
        ILogger ForType<T>();
        //
        // Summary:
        //     Logs an info message.
        //
        // Parameters:
        //   message:
        //     The message to log.
        //
        //   formatArgs:
        //     String.Format arguments (if applicable).
        void Info(string message, params object[] formatArgs);
        //
        // Summary:
        //     Logs a warning.
        //
        // Parameters:
        //   message:
        //     The message to log.
        //
        //   formatArgs:
        //     String.Format arguments (if applicable).
        void Warning(string message, params object[] formatArgs);
    }
}
