//namespace KrTrade.Nt.Services
//{
//    /// <summary>
//    /// Define the log options for any service.
//    /// </summary>
//    public class ServicesLogOptions
//    {

//        private string _label;

//        /// <summary>
//        /// True, if the the print service is enable.
//        /// </summary>
//        public bool IsEnable {  get; set; } = true;

//        /// <summary>
//        /// True, if the service log label is visible.
//        /// </summary>
//        public bool IsLabelVisible { get; set; } = false;

//        /// <summary>
//        /// The log label for the service.
//        /// </summary>
//        public string Label 
//        {
//            get => IsLabelVisible ? _label : string.Empty;
//            set
//            {
//                if (_label != value)
//                {
//                    _label = "[" + value.Trim() + "]: ";
//                }
//            }
//        }

//        /// <summary>
//        /// The separator for the diferent states in the same line.
//        /// </summary>
//        public string StatesSeparator {  get; set; } = " - ";

//        /// <summary>
//        /// Create <see cref="ServicesLogOptions"/> default instance.
//        /// </summary>
//        public ServicesLogOptions() 
//        { 
//        }

//        /// <summary>
//        /// Create <see cref="ServicesLogOptions"/> instance with specified values.
//        /// </summary>
//        /// <param name="label">The service label to show.</param>
//        public ServicesLogOptions(string label) : this(true,true,label," - ")
//        {
//        }

//        /// <summary>
//        /// Create <see cref="ServicesLogOptions"/> instance with specified values.
//        /// </summary>
//        /// <param name="label">The service label to show.</param>
//        /// <param name="statesSeparator">The string separator between states.</param>
//        public ServicesLogOptions(string label, string statesSeparator) : this(true,true,label,statesSeparator)
//        {
//        }

//        /// <summary>
//        /// Create <see cref="ServicesLogOptions"/> instance with specified values.
//        /// </summary>
//        /// <param name="isEnable">True if the log service is enable.</param>
//        /// <param name="isLabelVisible">True, if we want to show the service label.</param>
//        /// <param name="label">The service label to show.</param>
//        /// <param name="statesSeparator">The string separator between states.</param>
//        public ServicesLogOptions(bool isEnable, bool isLabelVisible, string label, string statesSeparator) 
//        { 
//            IsEnable = isEnable;
//            IsLabelVisible = isLabelVisible;
//            Label = label;     
//            StatesSeparator = statesSeparator;
//        }


//    }
//}
