namespace MobileTemplate.Core.Messages
{
    using MvvmCross.Plugin.Messenger;
    using MobileTemplate.Models;


    /// <summary>
    /// Message sent when the user is updated 
    /// </summary>
    public class UserUpdatedMessage : MvxMessage
    {
        /// <summary>
        /// New user information
        /// </summary>
        public User User { get; }


        /// <summary>
        /// Constructor
        /// </summary>
        public UserUpdatedMessage(object sender, User user) : base(sender)
        {
            this.User = user;
        }
    }
}
