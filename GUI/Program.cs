namespace GUI
{
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
        /// Returns the one DemoApplicationContext.
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
            Application.Run(new SpreadsheetGUI());
        }
    }
}