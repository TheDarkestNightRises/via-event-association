using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guest.GuestErrors;

public static class GuestAggregateErrors
{
    public static class FirstName
    {
        private const string Code = "Guest.FirstName";

        public static Error FirstNameCantBeEmpty => new(Code, "First name cannot be empty");
        public static Error FirstNameTooShort => new(Code, "First name should contain more than 2 characters");
        public static Error FirstNameTooLong => new(Code, "First name should contain less than 25 characters");
        public static Error FirstNameContainsInvalidCharacters => new(Code, "Last name cannot contain invalid characters");
    }
    
    public static class LastName
    {
        private const string Code = "Guest.LastName";

        public static Error LastNameCantBeEmpty => new(Code, "First name cannot be empty");
        public static Error LastNameTooShort => new(Code, "Last name should contain more than 2 characters");
        public static Error LastNameTooLong => new(Code, "First name should contain less than 25 characters");
        public static Error LastNameContainsInvalidCharacters => new(Code, "Last name cannot contain invalid characters");
    }
    
    public static class ViaEmail
    {
        private const string Code = "Guest.ViaEmail";

        public static Error EmailCantBeEmpty => new(Code, "Email cannot be empty");
        public static Error WrongDomain => new(Code, "Only people with VIA email can register");
        public static Error InvalidEmailFormat => new(Code, "Email must contain 3 or 4 uppercase/lowercase English letters, or 6 digits from 0 to 9");
        public static Error UsernameOutOfLength => new(Code, "Via email username is is between 3 and 6 (inclusive) characters long");
    }

    public static class PictureUrl
    {
        private const string Code = "Guest.PictureUrl";

        public static Error PictureCantBeEmpty => new(Code, "Picture cannot be empty");
    }
    
    public static class Id
    {
        private const string Code = "Guest.Id";

        public static Error InvalidId => new(Code, "Invalid ID");
    }
}