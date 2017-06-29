using System;
using System.Text;

namespace Codejnki.Utility.MongoRepository.Helpers
{
  /// <summary>
  /// Helper class with some useful methods
  /// </summary>
  public static class NotificationHelper
  {
    /// <summary>
    /// Creates the exception notification
    /// </summary>
    /// <param name="functionname">The functionname.</param>
    /// <param name="context">The context.</param>
    /// <param name="exception">The ex.</param>
    /// <returns></returns>
    public static string NotifyException(string functionname, string context, Exception exception)
    {
      string source = functionname + ": " + context;
      source = GetAllInformation(exception, source);
      return source;
    }

    /// <summary>
    /// Gets all information.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <param name="source">The source.</param>
    /// <returns></returns>
    private static string GetAllInformation(Exception exception, string source)
    {
      var sb = new StringBuilder();
      sb.AppendLine("********** " + DateTime.Now.ToString() + "**********");

      while (exception != null)
      {
        sb.AppendLine("Inner Exception Type: ");
        sb.AppendLine(exception.InnerException.GetType().ToString());
        sb.AppendLine("Inner Exception: ");
        sb.AppendLine(exception.InnerException.Message);
        sb.AppendLine("Inner Source: ");
        sb.AppendLine(exception.InnerException.Source);
        if (exception.InnerException.StackTrace != null)
        {
          sb.AppendLine("Inner Stack Trace: ");
          sb.AppendLine(exception.InnerException.StackTrace);
        }
        sb.AppendLine("Exception Type: ");
        sb.AppendLine(sb.GetType().ToString());
        sb.AppendLine("Exception: " + exception.Message);
        sb.AppendLine("Source: " + source);
        sb.AppendLine("Stack Trace: ");
        if (exception.StackTrace != null)
        {
          sb.AppendLine(exception.StackTrace);
          sb.AppendLine();
        }
        exception = exception.InnerException;
      }

      return sb.ToString();
    }
  }
}
