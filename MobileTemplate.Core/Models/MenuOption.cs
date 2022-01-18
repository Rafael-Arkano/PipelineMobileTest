namespace MobileTemplate.Core.Models
{
    using System.Windows.Input;

    /// <summary>
    /// A menu option
    /// </summary>
    public class MenuOption
    {
        /// <summary>
        /// Menu text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Descripcion
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// Command to execute
        /// </summary>
        public ICommand Command { get; set; }


        /// <summary>
        /// Icon or ghyph to use
        /// </summary>
        public string Icon { get; set; }
    }
}
