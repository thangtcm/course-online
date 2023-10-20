using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IIG_School.Helpers
{
    public static class ManageMenuPage
    {
        public static string Home => "Home";

        public static string About => "Exam";
        public static string Courses => "UserExam";
        public static string ExamTest => "ExamTest";

        public static string MainNavClass(ViewContext viewContext, string controllerName, string? actionName = null)
        {
            var currentController = viewContext.RouteData.Values["controller"] as string;
            var currentAction = viewContext.RouteData.Values["action"] as string;

            if (string.Equals(currentController, controllerName, StringComparison.OrdinalIgnoreCase) &&
                (actionName == null || string.Equals(currentAction, actionName, StringComparison.OrdinalIgnoreCase)))
            {
                return "";
            }

            return "collapsed";
        }

    }
}
