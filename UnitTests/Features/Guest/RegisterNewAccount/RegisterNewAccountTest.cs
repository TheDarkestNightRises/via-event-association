using ViaEventAssociation.Core.Domain.Aggregates.Guest.GuestErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

namespace UnitTests.Features.Guest.RegisterNewAccount;

public class RegisterNewAccountTest
{
    //UC10.S1
    
    //UC10.F1
    [Fact]
    public void GivenInvalidDomainEmail_WhenCreatingViaEmail_ShouldReturnFailureResultWithWrongDomain()
    {
        // Arrange
        var email = "321312@outlook.com";

        // Act
        var result = GuestViaEmail.Create(email);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == GuestAggregateErrors.ViaEmail.WrongDomain);
    }
    
    //UC10.F2
    [Theory]
    [InlineData("invalidemail@via.dk")]
    [InlineData("invalid.email@via.dk")]
    [InlineData("invalid@via.dk")]
    [InlineData("invalidemail@via@via.dk")]

    public void GivenInvalidEmailFormat_WhenCreatingViaEmail_ShouldReturnFailureResultWithInvalidEmailFormat(string arg)
    {
        // Arrange
        var email = arg;

        // Act
        var result = GuestViaEmail.Create(email);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == GuestAggregateErrors.ViaEmail.InvalidEmailFormat);
    }
    
    [Theory]
    [InlineData("us@via.dk")]      
    [InlineData("usernameee@via.dk")]  
    public void GivenInvalidUsernameLength_WhenCreatingViaEmail_ShouldReturnFailureResultWithUsernameOutOfLength(string arg)
    {
        // Arrange
        var email = arg;

        // Act
        var result = GuestViaEmail.Create(email);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == GuestAggregateErrors.ViaEmail.UsernameOutOfLength);
    }
    
    //UC10.F3
    [Fact]
    public void GivenNullFirstName_WhenCreatingFirstName_ShouldReturnFailureResultWithCantBeNullError()
    {
        // Arrange
        string? nullDescription = null;

        // Act
        var result = GuestFirstName.Create(nullDescription);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == GuestAggregateErrors.FirstName.FirstNameCantBeEmpty);
    }

    [Fact]
    public void GivenLongFirstName_WhenCreatingFirstName_ShouldReturnFailureResultWithFirstNameTooLongError()
    {
        // Arrange
        var longFirstName = new string('A', 26);

        // Act
        var result = GuestFirstName.Create(longFirstName);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == GuestAggregateErrors.FirstName.FirstNameTooLong);
    }
    
    [Fact]
    public void GivenShortFirstName_WhenCreatingFirstName_ShouldReturnFailureResultWithFirstNameTooShortError()
    {
        // Arrange
        var shortFirstName = "a";

        // Act
        var result = GuestFirstName.Create(shortFirstName);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == GuestAggregateErrors.FirstName.FirstNameTooShort);
    }

    //UC10.F4
    [Fact]
    public void GivenNullLastName_WhenCreatingLastName_ShouldReturnFailureResultWithCantBeNullError()
    {
        // Arrange
        string? nullDescription = null;

        // Act
        var result = GuestLastName.Create(nullDescription);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == GuestAggregateErrors.LastName.LastNameCantBeEmpty);
    }

    [Fact]
    public void GivenLongLastName_WhenCreatingLastName_ShouldReturnFailureResultWithLastNameTooLongError()
    {
        // Arrange
        var longLastName = new string('A', 26);

        // Act
        var result = GuestLastName.Create(longLastName);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == GuestAggregateErrors.LastName.LastNameTooLong);
    }
    
    [Fact]
    public void GivenShortLastName_WhenCreatingLastName_ShouldReturnFailureResultWithLastNameTooShortError()
    {
        // Arrange
        var shortLastName = "a";

        // Act
        var result = GuestLastName.Create(shortLastName);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == GuestAggregateErrors.LastName.LastNameTooShort);
    }
    
    //UC10.F5 

    //UC10.F6
    [Theory]
    [InlineData("231321312")]
    [InlineData("sasd223")]
    [InlineData("32blaba3e21")]
    public void GivenInvalidFirstNameWithNumbers_WhenCreatingFirstName_ShouldReturnFailureResultWithInvalidCharacters(string arg)
    {
        // Arrange
        var invalidLastName = arg;
        
        // Act
        var result = GuestFirstName.Create(invalidLastName);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == GuestAggregateErrors.FirstName.FirstNameContainsInvalidCharacters);
    }
    
    [Theory]
    [InlineData("231321312")]
    [InlineData("sasd223")]
    [InlineData("32blaba3e21")]
    public void GivenInvalidLastNameWithNumbers_WhenCreatingLastName_ShouldReturnFailureResultWithInvalidCharacters(string arg)
    {
        // Arrange
        var invalidLastName = arg;
        
        // Act
        var result = GuestLastName.Create(invalidLastName);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == GuestAggregateErrors.LastName.LastNameContainsInvalidCharacters);
    }
    
    //UC10.F7
    [Theory]
    [InlineData("&&#@*")]
    [InlineData("awsdaa@")]
    [InlineData("a$s#!@")]
    public void GivenInvalidFirstNameWithSymbols_WhenCreatingFirstName_ShouldReturnFailureResultWithInvalidCharacters(string arg)
    {
        // Arrange
        var invalidLastName = arg;
        
        // Act
        var result = GuestFirstName.Create(invalidLastName);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == GuestAggregateErrors.FirstName.FirstNameContainsInvalidCharacters);
    }
    
    [Theory]
    [InlineData("&&#@*")]
    [InlineData("awsdaa@")]
    [InlineData("a$s#!@")]
    public void GivenInvalidLastNameWithSymbols_WhenCreatingLastName_ShouldReturnFailureResultWithInvalidCharacters(string arg)
    {
        // Arrange
        var invalidLastName = arg;
        
        // Act
        var result = GuestLastName.Create(invalidLastName);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error == GuestAggregateErrors.LastName.LastNameContainsInvalidCharacters);
    }
}