UI Automation Testing with Selenium C#


It uses:

* [C#](https://learn.microsoft.com/en-us/dotnet/csharp/) as the programming language
* [Selenium WebDriver](https://www.selenium.dev/) for browser automation
* [Google Chrome](https://www.google.com/chrome/downloads/) as the local browser for testing
* [NuGet](https://www.nuget.org/) for dependency management
* [MSTest](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest) as the core test framework
* [Applitools Eyes](https://applitools.com/platform/eyes/) for visual testing
* [Applitools Ultrafast Grid](https://applitools.com/platform/ultrafast-grid/) for cross-browser execution


To run this project, you'll need:

1. An [Applitools account](https://auth.applitools.com/users/register), which you can register for free.
2. A good C# editor, such as [Microsoft Visual Studio](https://visualstudio.microsoft.com/).
3. The [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) SDK (which may come bundled with Visual Studio).
4. An up-to-date version of [Google Chrome](https://www.google.com/chrome/downloads/).

Tests Scenarios Automated:

Cart:

1. Add Product To Cart
2. Add Multiples Products to Cart and Validate Total
3. Delete Product from Cart

Login:

1. Create User and Login
2. Create User and Login with Leading Spaces
3. Create User and Login with Trailing Spaces
4. Create User and Login Using Empty Password
5. Create User and Login Using Empty User
6. Login User With More than 500 characters

Order:

1. Edit and Place Order (End to End Test)
2. Place Order (End to End Test)
3. Place Order With Empty Cart
4. Place Order With Empty values

Product Store:

1. Validate Carousel Is Displayed
2. Validate Existing Categories
3. Validate Page Footer
4. Validate Product Are Displayed
5. Validate Product Pagination
6. Validate Product Are Displayed for Each Category

SignUp:

1. Create User
2. Create User with Leading Spaces
3. Create User with No Valid Characters
4. Create User with Trailing Spaces
5. Create User with Empty Password
6. Create User with Empty UserName
7. Create User with Empty Values
8. Create User with More than 500 Characters
