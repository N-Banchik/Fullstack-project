namespace DataAccess.ErrorHandling
{
    internal static class ErrorMessages
    {
        internal static string InvalidId = "Invalid Id";
        internal static string InvalidEmailPassword = "No User found with this Email and Password combination, Please Double check Email and Password";
        internal static string LockedOut = "Cannot sign-in right now.";
        internal static string EmailInUse = "Email is already in use.";
        internal static string HobbExist = "Hobby already exist.";
        internal static string HobbyNotFound = "No Hobby found.";
        internal static string UserNotFound = "No User found.";
        internal static string CategoryNotFound = "No Category found.";
        internal static string CategoryExist = "Category already exist.";
        internal static string GuideNotFound = "No Guide found.";
        internal static string GuideWithNoContent = "Guide has no content.";
        internal static string NotGuideCreator = "You are not the creator of this guide.";
        internal static string EventNotFound = "No Event found.";
        internal static string PostNotFound = "No Post found.";
        internal static string PhotoUploadError = "Photo upload failed.";
        internal static string AlreadyRegistered = "You are already registered for this event.";
        internal static string NotRegistered = "You are not registered for this event.";
        internal static string AlreadyFollow = "You are already following this Hobby.";
        internal static string PasswordChangeFailed = "Password change failed.";
        internal static string PasswordSameAsOld = "the password can't be the same as the old one.";
        internal static string PhotoUploadFailed = "Could not upload photo";
        internal static string PhotoDeletionFailed = "Could not delete photo";
        internal static string PhotoNotFound = "Photo not found";
        internal static string PhotoIsMain = "Can't delete main photo";



    }
}
