 namespace GUI
{
    /// <summary>
    /// Author:     Joe Zachary
    /// Updated by: Jim de St. Germain
    ///  
    ///  Dates:      (Original) 2012-ish
    ///              (Updated for Core) 2020
    ///              
    ///  Target: ASP CORE 3.1
    ///  
    ///  This program is an example of how to create a GUI application for 
    ///  a spreadsheet project.
    ///  
    ///  It relies on a working Spreadsheet Panel class, but defines other
    ///  GUI elements, such as the file menu (open and close operations).
    ///  
    /// 
    /// This code was borrowed from the Example GUI code created by Prof. Joe Zachary and Prof. de St. Germain
    /// Abhiveer and I were able to understand this code and how it helped us open multiple forms for our Spreadsheet GUI,
    /// so we decided to use it with citation of course.
    /// 
    /// </summary>
    class Spreadsheet_Window : ApplicationContext
    {
        /// <summary>
        ///  Number of open forms
        /// </summary>
        private int formCount = 0;

        /// <summary>
        ///  Singleton ApplicationContext
        /// </summary>
        private static Spreadsheet_Window appContext;

        /// <summary>
        /// Returns the one ApplicationContext.
        /// </summary> 
        public static Spreadsheet_Window getAppContext()
        {
            if (appContext == null)
            {
                appContext = new Spreadsheet_Window();
            }
            return appContext;
        }

        /// <summary>
        /// Private constructor for singleton pattern
        /// </summary>
        private Spreadsheet_Window()
        {
        }

        /// <summary>
        /// Build another GUI Window
        /// </summary>
        public void RunForm(Form form)
        {
            // One more form is running
            formCount++;

            // Assign an EVENT handler to take an action when the GUI is closed 
            form.FormClosed += (o, e) => { if (--formCount <= 0) ExitThread(); };

            // Run the form
            form.Show();
        }

    }


    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Start an application context and run one form inside it. Closing the first form does not close the others
            Spreadsheet_Window appContext = Spreadsheet_Window.getAppContext();
            appContext.RunForm(new SpreadsheetGUI());
            Application.Run(appContext);
        }
    }
}